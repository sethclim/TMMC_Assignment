using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using TMMCVerticalLineCounterApp;
using TMMCVerticalLineCounterApp.Services;

var fileNameOption = new Option<string>(
    "--fileName")
{
    Required = true,
    Description = "Path/to/file"
};

var rootCommand = new RootCommand("My Vertical Line Counter App")
{
    fileNameOption
};

rootCommand.SetAction(parseResult =>
{
    try
    {
        string fileName = parseResult.GetValue(fileNameOption)!;

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("Exactly one command-line argument is required (e.g., --fileName <path/to/file>)");

        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                _ = config.AddCommandLine([$"--fileName={fileName}"]);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IImageLoader, JpegImageLoader>();
                services.AddSingleton<App>();
            })
            .Build();

        var app = host.Services.GetRequiredService<App>();
        app.Run().GetAwaiter().GetResult();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Fatal error: {ex.Message}");
        Environment.Exit(1);
    }
});

var parseResult = rootCommand.Parse(args);
parseResult.Invoke();
