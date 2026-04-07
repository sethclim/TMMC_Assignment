using Microsoft.Extensions.Configuration;

namespace TMMCVerticalLineCounterApp
{
    internal class App
    {
        private readonly IConfiguration _config;

        public App(IConfiguration config)
        {
            Console.WriteLine("Initializing App...");
            _config = config;
        }

        public Task Run()
        {
            Console.WriteLine("Running App...");

            var fileName = _config["filename"];

            Console.WriteLine($"Hello {fileName}!");

            return Task.CompletedTask;
        }
    }
}

