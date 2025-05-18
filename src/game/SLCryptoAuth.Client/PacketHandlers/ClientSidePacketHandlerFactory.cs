using System;
using SLCryptoAuth.Client.PacketHandlers.V1;
using SLCryptoAuth.Cryptography.DiffieHellman;
using SLCryptoAuth.Cryptography.DigitalSignature;
using SLCryptoAuth.Network.Handlers;

namespace SLCryptoAuth.Client.PacketHandlers;

public class ClientSidePacketHandlerFactory : PacketHandlerFactory
{
    public ClientSidePacketHandlerFactory(Ecdsa identityEcdsa, Func<Ecdh> getSessionEcdh)
    {
        RegisterHandler(new ServerHandshakeHandlerV1(identityEcdsa, getSessionEcdh));
    }
}
