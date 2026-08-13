using Next.Hazel.Abstractions;
using Reactor.Networking;

namespace Reactor.Empostor.Net;

public static class ModdedHandshakeS2C
{
    public static void Serialize(IMessageWriter writer, string serverName, string serverVersion, int pluginCount)
    {
        writer.StartMessage(byte.MaxValue);
        writer.Write((byte) ReactorMessageFlags.Handshake);
        writer.Write(serverName);
        writer.Write(serverVersion);
        writer.WritePacked(pluginCount);
        writer.EndMessage();
    }
}
