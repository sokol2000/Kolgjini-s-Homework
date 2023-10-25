using System.Drawing;
using System.Drawing.Imaging;

namespace Question_3_CSharp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int N = 100000; // Number of random variates
            int k = 100;   // Number of intervals

            // Create an array to store the histogram values
            int[] histogram = new int[k];

            Random random = new Random();

            for (int i = 0; i < N; i++)
            {
                double randomValue = random.NextDouble(); // Random value in [0, 1)
                int interval = (int)(randomValue * k);
                histogram[interval]++;
            }

            int width = 800;
            int height = 500;
            Bitmap bitmap = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Set up the drawing parameters
                int maxCount = histogram[0];
                for (int i = 1; i < k; i++)
                {
                    if (histogram[i] > maxCount)
                    {
                        maxCount = histogram[i];
                    }
                }

                int barWidth = width / k;

                for (int i = 0; i < k; i++)
                {
                    int barHeight = (int)((double)histogram[i] / maxCount * height);
                    int x = i * barWidth;
                    int y = height - barHeight;
                    int barY = y;
                    int barHeightFill = barHeight;

                    using (SolidBrush brush = new SolidBrush(Color.Aquamarine))
                    {
                        graphics.FillRectangle(brush, x, barY, barWidth, barHeightFill);
                    }
                }
            }

            bitmap.Save("histogram.png", ImageFormat.Png);
        }
    }
}