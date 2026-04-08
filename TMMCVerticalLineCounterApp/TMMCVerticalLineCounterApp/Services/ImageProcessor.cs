    using Microsoft.Extensions.Logging;
    using TMMCVerticalLineCounterApp.Models;

    namespace TMMCVerticalLineCounterApp.Services
    {
        public class ImageProcessor(ILoggerFactory loggerFactory) : IImageProcessor
        {
            private readonly ILogger _logger = loggerFactory.CreateLogger(nameof(ImageProcessor));
            private static readonly int _blackThreshold = 10;

            /// <summary>
            /// Detects which columns contain a vertical black bar.
            /// Only checks the top half of the image, leveraging the fact that lines are vertically symmetrical.
            /// A column is considered a black bar if **any black pixel** is found in the top half of that column.
            /// The loop breaks early once a black pixel is found for efficiency.
            /// </summary>
            /// <param name="image">ImageData containing Width, Height, and RGBA pixels</param>
            /// <returns>Array where each index corresponds to whether the column contains a black bar (true/false)</returns>
            static bool[] DetectBlackBarsPerColumn(ImageData image)
            {
                bool[] res = new bool[image.Width];
                int halfCeil = (image.Height + 1) / 2;

                for (int x = 0; x < image.Width; x++)
                { 
                    bool found = false;
                    for (int y = 0; y < halfCeil; y++)
                    {
                        int i = (y * image.Width + x) * 4;

                        if (image.Pixels[i] <= _blackThreshold && 
                            image.Pixels[i + 1] <= _blackThreshold && 
                            image.Pixels[i + 2] <= _blackThreshold)
                        {
                            found = true;
                            break;
                        }
                    }
                    res[x] = found;
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

                bool[] detectedBars = DetectBlackBarsPerColumn(image);

                int barCount = 0;
                bool prev = false;
                for (int i = 0; i < detectedBars.Length; i++)
                {
                    bool potentialBar = detectedBars[i];

                    // entered a bar, count it
                    if(potentialBar && !prev)
                        barCount++;
                    prev = potentialBar;
                }

                return barCount;
            }
        }
    }
