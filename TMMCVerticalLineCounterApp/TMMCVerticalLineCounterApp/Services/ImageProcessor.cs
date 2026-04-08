using Microsoft.Extensions.Logging;
using TMMCVerticalLineCounterApp.Models;

namespace TMMCVerticalLineCounterApp.Services
{
    public class ImageProcessor(ILoggerFactory loggerFactory) : IImageProcessor
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger(nameof(ImageProcessor));
        private readonly static int _lineLengthMinThreshold = 1;
        

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
                int count = 0;
                for (int y = 0; y < image.Height / 2; y++)
                {
                    int i = (y * image.Width + x) * 4;

                    if (image.Pixels[i] == 0 && image.Pixels[i + 1] == 0 && image.Pixels[i + 2] == 0)
                        count++;

                    if(count > _lineLengthMinThreshold)
                        break;
                }
                res[x] = count;
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

            if (image.Width == 0 || image.Height == 0 || image.Pixels.Length == 0)
                throw new ArgumentException("Image cannot be empty.", nameof(image));

            int[] signal = ConvertToSignal(image);

            int barCount = 0;
            int prev = 0;
            for (int i = 0; i < signal.Length; i++)
            {
                int s = signal[i];

                // entered a bar, count it
                if(s > 0 && prev == 0)
                    barCount++;
                prev = s;
            }

            return barCount;
        }
    }
}
