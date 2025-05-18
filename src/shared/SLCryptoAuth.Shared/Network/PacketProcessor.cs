using SLCryptoAuth.IO;
using SLCryptoAuth.Network.DTO;
using SLCryptoAuth.Network.Handlers;

namespace SLCryptoAuth.Network;

public class PacketProcessor(PacketHandlerFactory handlerFactory)
{
    public AuthResult ProcessPacket(byte[] packet, object? state)
    {
        var packetReader = new BinaryReaderExtended(packet);

        if (packetReader.Length < 2)
            return AuthResult.InvalidPacket(packet);

        var id = packetReader.ReadByte();
        var version = packetReader.ReadByte();
        var payloadReader = new BinaryReaderExtended(packetReader.ReadRemainingBytes());

        return handlerFactory.TryGetHandler(id, version, out var handler)
            ? handler.ProcessPayload(payloadReader, state)
            : AuthResult.InvalidPacket(packet);
    }
}
