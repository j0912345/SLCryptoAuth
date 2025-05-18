using LiteNetLib;
using LiteNetLib.Utils;
using SLCryptoAuth.IO;
using SLCryptoAuth.Network.DTO;
using SLCryptoAuth.Network.Handlers;

namespace SLCryptoAuth.Server.Core;

public abstract class ServerAuthPacketHandler : IPacketHandler
{
    /// <inheritdoc />
    public abstract byte Id { get; }

    /// <inheritdoc />
    public abstract byte Version { get; }

    /// <inheritdoc />
    public AuthResult ProcessPayload(BinaryReaderExtended payloadReader, object state)
    {
        if (state is not ConnectionRequest request)
            throw new InvalidTypeException("state must be ConnectionRequest");
        
        return ProcessPayload(payloadReader, request);
    }

    public abstract AuthResult ProcessPayload(BinaryReaderExtended payloadReader, ConnectionRequest request);
}
