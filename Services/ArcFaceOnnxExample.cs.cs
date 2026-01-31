using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using OpenCvSharp;
using System;
using System.Linq;

namespace FaceFortify.Examples
{
    public static class ArcFaceOnnxExample
    {
        public static float[] GetEmbedding(Mat faceBgr)
        {
            // Resize to model input (112x112)
            using var resized = faceBgr.Resize(new Size(112, 112));

            // Convert BGR → RGB
            Cv2.CvtColor(resized, resized, ColorConversionCodes.BGR2RGB);

            // Normalize to [-1, 1]
            var input = new DenseTensor<float>(new[] { 1, 3, 112, 112 });

            for (int y = 0; y < 112; y++)
            {
                for (int x = 0; x < 112; x++)
                {
                    var pixel = resized.At<Vec3b>(y, x);

                    input[0, 0, y, x] = (pixel.Item0 / 127.5f) - 1f; // R
                    input[0, 1, y, x] = (pixel.Item1 / 127.5f) - 1f; // G
                    input[0, 2, y, x] = (pixel.Item2 / 127.5f) - 1f; // B
                }
            }

            using var session = new InferenceSession("arcface.onnx");

            var inputs = new[]
            {
                NamedOnnxValue.CreateFromTensor("input", input)
            };

            using var results = session.Run(inputs);

            // 512-d embedding
            var embedding = results.First().AsEnumerable<float>().ToArray();

            return L2Normalize(embedding);
        }

        private static float[] L2Normalize(float[] v)
        {
            float sum = 0f;
            foreach (var x in v) sum += x * x;
            float norm = (float)Math.Sqrt(sum);

            return v.Select(x => x / norm).ToArray();
        }
    }
}
