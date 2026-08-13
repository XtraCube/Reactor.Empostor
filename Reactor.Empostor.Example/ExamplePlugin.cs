using Empostor.Api.Events;
using Empostor.Api.Plugins;
using Reactor.Empostor.Rpcs;

namespace Reactor.Empostor.Example;

[EmpostorPlugin("gg.reactor.Empostor.example")]
public class ExamplePlugin : PluginBase
{
    private readonly IReactorCustomRpcManager _rpcManager;

    private MultiDisposable? _disposable;

    public ExamplePlugin(IReactorCustomRpcManager rpcManager)
    {
        _rpcManager = rpcManager;
    }

    public override ValueTask EnableAsync()
    {
        _disposable = new MultiDisposable(
            _rpcManager.Register<ExampleRpc>()
        );

        return default;
    }

    public override ValueTask DisableAsync()
    {
        _disposable?.Dispose();

        return default;
    }
}