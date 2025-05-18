using System;
using System.Collections.Generic;

namespace SLCryptoAuth.Network.Handlers;

public class PacketHandlerFactory
{
    private readonly Dictionary<(byte, byte), IPacketHandler> _handlers = [];

    public void RegisterHandler(IPacketHandler handler)
    {
        if (_handlers.ContainsKey((handler.Id, handler.Version)))
            throw new Exception($"Handler for packet {handler.Id}/{handler.Version} already registered.");

        _handlers.Add((handler.Id, handler.Version), handler);
    }

    public bool TryGetHandler(byte id, byte version, out IPacketHandler handler)
    {
        return _handlers.TryGetValue((id, version), out handler);
    }
}
