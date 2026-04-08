using TMMCVerticalLineCounterApp.Models;

namespace TMMCVerticalLineCounterApp.Services
{
    internal class ImageProcessor : IImageProcessor
    {
        int[] ConvertToSignal(ImageData image)
        {
            int[] res = new int[image.Width];
            Console.WriteLine($"res: {res.Length}");
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    int i = (y * image.Width + x) * 4;
                    byte r = image.Pixels[i + 0];
                    byte g = image.Pixels[i + 1];
                    byte b = image.Pixels[i + 2];
                    byte a = image.Pixels[i + 3];

                    //Console.WriteLine($"Pixel ({x},{y}) = R:{r} G:{g} B:{b} A:{a}");

                    int sum = r + g + b;

                    //Console.WriteLine($"sum: {sum}");

                    int normalized = (int)(1f - (sum / 765f));

                    res[x] += normalized;

                }
                Console.WriteLine($"res: {res[x]}");
            }

            return res;
        }

        public int Process(ImageData image)
        {
            Console.WriteLine($"width: {image.Width}");

            int[] signal = ConvertToSignal(image);

            int count = 0;
            int prev = 0;
            for (int i = 0; i < signal.Length; i++)
            {
                int s = signal[i];

                if(s > 0 && prev == 0)
                {
                    Console.WriteLine($"entering bar!");
                    count++;
                }
                else if (s == 0 && prev > 0)
                {
                    Console.WriteLine($"left bar!");
                }
                prev = s;
            }

            return count;
        }
    }
}
