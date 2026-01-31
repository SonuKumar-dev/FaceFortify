using System;
using System.Linq;

namespace FaceFortify.Examples
{
    public static class FaceEmbeddingExample
    {
        // Compute cosine similarity between two embeddings
        public static double CosineSimilarity(float[] a, float[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Embedding sizes must match");

            double dot = 0;
            double magA = 0;
            double magB = 0;

            for (int i = 0; i < a.Length; i++)
            {
                dot += a[i] * b[i];
                magA += a[i] * a[i];
                magB += b[i] * b[i];
            }

            return dot / (Math.Sqrt(magA) * Math.Sqrt(magB));
        }

        // Simple demo
        public static void Demo()
        {
            // Fake 512-dim embeddings (shortened here for demo)
            float[] stored = Enumerable.Repeat(0.5f, 8).ToArray();
            float[] current = Enumerable.Repeat(0.52f, 8).ToArray();

            double similarity = CosineSimilarity(stored, current);

            Console.WriteLine($"Similarity: {similarity:F3}");

            if (similarity >= 0.45)
                Console.WriteLine("Face match");
            else
                Console.WriteLine("Face not match");
        }
    }
}
