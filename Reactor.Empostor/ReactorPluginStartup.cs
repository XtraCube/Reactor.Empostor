using Empostor.Api.Events;
using Empostor.Api.Http;
using Empostor.Api.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reactor.Empostor.Http;
using Reactor.Empostor.Rpcs;

namespace Reactor.Empostor;

public class ReactorPluginStartup : IPluginHttpStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IEventListener, HandshakeEventListener>();
        services.AddSingleton<IReactorCustomRpcManager, ReactorCustomRpcManager>();

        services.AddSingleton<IListingFilter, ReactorHandshakeFilter>();
        services.AddSingleton<ClientModsHeader>();
    }

    public void ConfigureWebApplication(IApplicationBuilder builder)
    {
        builder.UseMiddleware<ClientModsHeader>();
    }

    public void ConfigureHost(IHostBuilder host)
    {
    }
}
