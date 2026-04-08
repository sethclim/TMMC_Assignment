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
    Description = "Absolute path to file e.g. C:\\TMMC_interview_assignment\\img_1.jpg"
};

var rootCommand = new RootCommand("My Vertical Line Counter App")
{
    fileNameOption
};

/// <summary>
/// Sets up the root command to accept a single --fileName argument. 
/// Builds a DI host with services, retrieves and runs the main App instance. 
/// Any exceptions thrown during execution are caught and reported.
/// The parser automatically enforces that --fileName is provided.
/// </summary>
rootCommand.SetAction(parseResult =>
{
    try
    {
        string fileName = parseResult.GetValue(fileNameOption)!;

        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                _ = config.AddCommandLine([$"--fileName={fileName}"]);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IImageLoader, JpegImageLoader>();
                services.AddSingleton<IImageProcessor, ImageProcessor>();
                services.AddSingleton<App>();
            })
            .Build();

        var app = host.Services.GetRequiredService<App>();
        app.Run().GetAwaiter().GetResult();
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Fatal error: {ex.Message}");
        Environment.Exit(1);
    }
});

var parseResult = rootCommand.Parse(args);
parseResult.Invoke();
