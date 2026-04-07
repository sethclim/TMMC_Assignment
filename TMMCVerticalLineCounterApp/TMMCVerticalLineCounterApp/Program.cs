using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMMCVerticalLineCounterApp;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {  
        services.AddSingleton<App>();
    })
    .Build();

var app = host.Services.GetRequiredService<App>();

await app.Run();
