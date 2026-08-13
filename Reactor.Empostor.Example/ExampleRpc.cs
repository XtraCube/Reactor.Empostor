using Empostor.Api.Net;
using Empostor.Api.Net.Inner.Objects;
using Microsoft.Extensions.Logging;
using Next.Hazel.Abstractions;
using Reactor.Empostor.Rpcs;

namespace Reactor.Empostor.Example;

public class ExampleRpc : ReactorCustomRpc<IInnerPlayerControl>
{
    private readonly ILogger<ExampleRpc> _logger;
    private readonly IMessageWriterProvider _writerProvider;

    public ExampleRpc(ILogger<ExampleRpc> logger, IMessageWriterProvider writerProvider)
    {
        _logger = logger;
        _writerProvider = writerProvider;
    }

    public override string ModId => "gg.reactor.Example";

    public override uint Id => 0;

    public override async ValueTask<bool> HandleAsync(IInnerPlayerControl player, IClientPlayer sender, IClientPlayer? target, IMessageReader reader)
    {
        Deserialize(reader, out var text);

        _logger.LogInformation("{player} said {text}", player.PlayerInfo.PlayerName, text);
        await SendAsync(player, $"Send: from server responding to {player.PlayerInfo.PlayerName}");

        return true;
    }

    public ValueTask SendAsync(IInnerPlayerControl player, string text, IClient? targetClient = null)
    {
        using var data = _writerProvider.Get();
        Serialize(data, text);
        return base.SendAsync(player, data, targetClient?.Id ?? null);
    }

    public static void Serialize(IMessageWriter writer, string text)
    {
        writer.Write(text);
    }

    public static void Deserialize(IMessageReader reader, out string text)
    {
        text = reader.ReadString();
    }
}
