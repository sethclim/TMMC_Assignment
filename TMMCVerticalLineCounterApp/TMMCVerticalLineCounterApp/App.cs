using Microsoft.Extensions.Configuration;
using TMMCVerticalLineCounterApp.Services;

namespace TMMCVerticalLineCounterApp
{
    internal class App
    {
        private readonly IConfiguration _config;
        private readonly IImageLoader _loader;
        private readonly IImageProcessor _processor;

        public App(IConfiguration config, IImageLoader loader, IImageProcessor processor)
        {
            Console.WriteLine("Initializing App...");
            _config = config;
            _loader = loader;
            _processor = processor;
        }

        public Task Run()
        {
            Console.WriteLine("Running App...");

            string fileName = _config["fileName"]
               ?? throw new InvalidOperationException("fileName is required");

            Console.WriteLine($"Loading {fileName}!");

            Models.ImageData imageData = _loader.Load(fileName);

            Console.WriteLine($"imageData length: {imageData.Pixels.Length}!");

            _processor.Process(imageData);

            return Task.CompletedTask;
        }
    }
}

