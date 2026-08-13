using Empostor.Api.Events;
using Empostor.Api.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Reactor.Empostor.Example;

public class ExamplePluginStartup : IPluginStartup
{
    public void ConfigureHost(IHostBuilder host)
    {
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IEventListener, ExampleEventListener>();
    }
}