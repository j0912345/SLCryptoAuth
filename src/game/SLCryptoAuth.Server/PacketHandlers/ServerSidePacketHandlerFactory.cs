using SLCryptoAuth.Cryptography.DigitalSignature;
using SLCryptoAuth.Network.Handlers;
using SLCryptoAuth.Server.PacketHandlers.V1;

namespace SLCryptoAuth.Server.PacketHandlers;

public class ServerSidePacketHandlerFactory : PacketHandlerFactory
{
    public ServerSidePacketHandlerFactory(Ecdsa serverIdentity)
    {
        RegisterHandler(new AskServerHandshakeHandlerV1(serverIdentity));
        RegisterHandler(new ClientHandshakeHandlerV1());
    }
}
