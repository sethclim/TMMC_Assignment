    using Microsoft.Extensions.Logging;
    using TMMCVerticalLineCounterApp.Models;

    namespace TMMCVerticalLineCounterApp.Services
    {
        public class ImageProcessor(ILoggerFactory loggerFactory) : IImageProcessor
        {
            private readonly ILogger _logger = loggerFactory.CreateLogger(nameof(ImageProcessor));
            // minimum Length vertical line need to be longer than to be counted
            private readonly static int _lineLengthMinThreshold = 1;


            /// <summary>
            /// Detects which columns contain a vertical black bar
            /// Only checks the top half of the image leveraging vertically symmetrical input 
            /// Stops checking each column once a minimum threshold for line length is reached
            /// </summary>
            /// <param name="image">ImageData containing Width, Height, and RGBA pixels</param>
            /// <returns>Array where each index corresponds to a black column existing or not</returns>
            static bool[] DetectBlackBarsPerColumn(ImageData image)
            {
                bool[] res = new bool[image.Width];
                int halfCeil = (image.Height + 1) / 2;

                //if image is 1d or height of two, consider a "line" to be single pixel
                int lineLengthMinThreshold = image.Height < 2 ? 0 : _lineLengthMinThreshold;

                for (int x = 0; x < image.Width; x++)
                { 
                    int count = 0;
                    for (int y = 0; y < halfCeil; y++)
                    {
                        int i = (y * image.Width + x) * 4;

                        if (image.Pixels[i] == 0 && image.Pixels[i + 1] == 0 && image.Pixels[i + 2] == 0)
                            count++;

                        if(count > lineLengthMinThreshold)
                            break;
                    }
                    res[x] = count > lineLengthMinThreshold;
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

                // Print each column as 0/1 or T/F
                Console.WriteLine("Detected columns:");
                for (int x = 0; x < detectedBars.Length; x++)
                {
                    Console.Write(detectedBars[x] ? "1" : "0");
                }
                Console.WriteLine(); // newline at end

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
