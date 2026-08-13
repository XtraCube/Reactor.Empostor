using Empostor.Api.Events;
using Empostor.Api.Net.Custom;
using Empostor.Api.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Reactor.Empostor.Net;

namespace Reactor.Empostor;

[EmpostorPlugin("gg.reactor.Empostor")]
public class ReactorPlugin : PluginBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICustomMessageManager<ICustomRootMessage> _customRootMessageManager;
    private readonly ICustomMessageManager<ICustomRpc> _customRpcManager;

    private MultiDisposable? _disposable;

    public ReactorPlugin(IServiceProvider serviceProvider, ICustomMessageManager<ICustomRootMessage> customRootMessageManager, ICustomMessageManager<ICustomRpc> customRpcManager)
    {
        _serviceProvider = serviceProvider;
        _customRootMessageManager = customRootMessageManager;
        _customRpcManager = customRpcManager;
    }

    public override ValueTask EnableAsync()
    {
        _disposable = new MultiDisposable(
            _customRootMessageManager.Register(ActivatorUtilities.CreateInstance<Message255Reactor>(_serviceProvider)),
            _customRpcManager.Register(ActivatorUtilities.CreateInstance<Rpc255Reactor>(_serviceProvider))
        );

        return default;
    }

    public override ValueTask DisableAsync()
    {
        _disposable?.Dispose();

        return default;
    }
}