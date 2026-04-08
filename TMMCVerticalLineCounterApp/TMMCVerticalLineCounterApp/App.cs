using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TMMCVerticalLineCounterApp.Services;

namespace TMMCVerticalLineCounterApp
{
    internal class App
    {
        private readonly ILogger<App> _logger;
        private readonly IConfiguration _config;
        private readonly IImageLoader _loader;
        private readonly IImageProcessor _processor;

        public App(ILogger<App> logger, IConfiguration config, IImageLoader loader, IImageProcessor processor)
        {
            logger.LogInformation("Initializing App...");
            _config = config;
            _loader = loader;
            _processor = processor;
            _logger = logger;
        }

        /// <summary>
        /// Runs the app: validates the filePath, loads the image, counts vertical bars, and logs the result.
        /// </summary>
        public Task Run()
        {
            _logger.LogInformation("Running App...");

            string fileName = _config["fileName"]
               ?? throw new InvalidOperationException("fileName is required");


            if (!Path.IsPathFullyQualified(fileName))
            {
                _logger.LogError($"Supplied filePath must be absolute: {fileName}");
                return Task.FromResult(1);
            }

            Models.ImageData imageData = _loader.Load(fileName);

            int count = _processor.Process(imageData);

            _logger.LogInformation($"Number of vertical bars in {fileName} is {count}!");

            return Task.CompletedTask;
        }
    }
}

