using SLCryptoAuth.IO;
using SLCryptoAuth.Network.DTO;

namespace SLCryptoAuth.Network.Handlers;

public interface IPacketHandler
{
    byte Id { get; }
    byte Version { get; }

    AuthResult ProcessPayload(BinaryReaderExtended payloadReader, object? state);
}
