using OpenCvSharp;

namespace FaceFortify.Examples
{
    public static class HaarCascadeExample
    {
        public static void Run()
        {
            using var capture = new VideoCapture(0);
            using var window = new Window("Haar Face Detection");

            var faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");

            using var frame = new Mat();

            while (true)
            {
                capture.Read(frame);
                if (frame.Empty()) break;

                using var gray = new Mat();
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

                // Detect faces
                Rect[] faces = faceCascade.DetectMultiScale(
                    gray,
                    scaleFactor: 1.1,
                    minNeighbors: 5,
                    flags: HaarDetectionTypes.ScaleImage,
                    minSize: new Size(80, 80));

                // Draw rectangles
                foreach (var face in faces)
                {
                    Cv2.Rectangle(frame, face, Scalar.LimeGreen, 2);
                }

                window.ShowImage(frame);

                if (Cv2.WaitKey(1) == 27) // ESC to exit
                    break;
            }
        }
    }
}
