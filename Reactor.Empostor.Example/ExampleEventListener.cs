using Empostor.Api.Events;
using Empostor.Api.Events.Player;
using Reactor.Empostor.Rpcs;

namespace Reactor.Empostor.Example;

public class ExampleEventListener : IEventListener
{
    private readonly IReactorCustomRpcManager _rpcManager;

    public ExampleEventListener(IReactorCustomRpcManager rpcManager)
    {
        _rpcManager = rpcManager;
    }

    [EventListener]
    public async ValueTask OnPlayerSpawned(IPlayerSpawnedEvent e)
    {
        var clientInfo = e.ClientPlayer.Client.GetReactor();

        if (clientInfo != null && clientInfo.Mods.Any(x => x.Id == "gg.reactor.Example"))
        {
            await _rpcManager.Get<ExampleRpc>().SendAsync(e.PlayerControl, "SendTo: from server to spawned client", e.ClientPlayer.Client);
        }
    }
}