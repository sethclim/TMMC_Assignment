using Microsoft.Extensions.Logging;
using TMMCVerticalLineCounterApp.Models;

namespace TMMCVerticalLineCounterApp.Services
{
    internal class ImageProcessor(ILoggerFactory loggerFactory) : IImageProcessor
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger(nameof(ImageProcessor));

        /// <summary>
        /// Projects a 2D image onto a 1D horizontal signal by aggregating pixel brightness vertically.
        /// For each column, pixel RGB values are converted to a normalized darkness value,
        /// then summed to produce a column intensity. 
        /// </summary>
        /// <param name="image">ImageData containing Width, Height, and RGBA pixels</param>
        /// <returns>Array where each index corresponds to a column intensity</returns>
        static int[] ConvertToSignal(ImageData image)
        {
            int[] res = new int[image.Width];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    int i = (y * image.Width + x) * 4;
                    byte r = image.Pixels[i + 0];
                    byte g = image.Pixels[i + 1];
                    byte b = image.Pixels[i + 2];
                    byte a = image.Pixels[i + 3];

                    int sum = r + g + b;

                    int normalized = (int)(1f - (sum / 765f));

                    res[x] += normalized;

                }
            }

            return res;
        }

        /// <summary>
        /// Counts the number of black bars in the supplied image data
        /// </summary>
        /// <param name="image">data from image to process</param>
        /// <returns>number of counted bars</returns>
        public int Process(ImageData image)
        {
            _logger.LogInformation("Processing image...");
            int[] signal = ConvertToSignal(image);

            int count = 0;
            int prev = 0;
            for (int i = 0; i < signal.Length; i++)
            {
                int s = signal[i];

                // entered a bar, count it
                if(s > 0 && prev == 0)
                {
                    count++;
                }
                prev = s;
            }

            return count;
        }
    }
}
