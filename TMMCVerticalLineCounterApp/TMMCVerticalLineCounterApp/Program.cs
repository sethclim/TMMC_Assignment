using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMMCVerticalLineCounterApp;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {  
        services.AddSingleton<App>();
    })
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddCommandLine(args);
    })
    .Build();

var app = host.Services.GetRequiredService<App>();
var configuration = host.Services.GetRequiredService<IConfiguration>();

await app.Run();
