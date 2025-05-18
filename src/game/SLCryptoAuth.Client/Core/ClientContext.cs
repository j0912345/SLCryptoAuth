using System;
using System.Collections.Generic;
using System.IO;
using SLCryptoAuth.Client.PacketHandlers;
using SLCryptoAuth.Cryptography.DiffieHellman;
using SLCryptoAuth.Cryptography.DigitalSignature;
using SLCryptoAuth.IO;
using SLCryptoAuth.Network;
using SLCryptoAuth.Network.DTO;

namespace SLCryptoAuth.Client.Core;

public class ClientContext
{
    private static readonly string SLCryptoAuthDirectoryPath =  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SCP Secret Laboratory", "SLCryptoAuth");
    private static readonly string IdentityPrivateKeyPersistentPath = Path.Combine(SLCryptoAuthDirectoryPath, "identity_private.key");
    
    public static ClientContext Instance { get; } = new();

    private readonly Dictionary<string, byte[]> _savedState = new();
    
    private readonly TrustContext _trustContext;
    private readonly PersistentKeyStorage _persistentKeyStorage;
    private readonly PacketProcessor _packetProcessor;
    private readonly Ecdsa _identityEcdsa;
    
    private Ecdh _sessionEcdh = new();

    private ClientContext()
    {
        _trustContext = new TrustContext();
        _persistentKeyStorage = new PersistentKeyStorage(IdentityPrivateKeyPersistentPath);

        Directory.CreateDirectory(SLCryptoAuthDirectoryPath);
        if (!_persistentKeyStorage.TryGetIdentityPrivateKey(out var identityPrivateKey))
        {
            _identityEcdsa = new Ecdsa();
            _persistentKeyStorage.SaveIdentityPrivateKey(_identityEcdsa.PrivateKeyBytes);
        }
        else
        {
            _identityEcdsa = new Ecdsa(identityPrivateKey);
        }

        var handlerFactory = new ClientSidePacketHandlerFactory(_identityEcdsa, () => _sessionEcdh);
        _packetProcessor = new PacketProcessor(handlerFactory);
    }

    public byte[] GetConnectData(string serverIp)
    {
        return _savedState.Remove(serverIp, out var requestData)
            ? requestData
            : GenerateClientHandshake();
    }

    private byte[] GenerateClientHandshake()
    {
        _sessionEcdh = new Ecdh();
        var connectData = new List<byte> { 50, 1 };
        connectData.AddRange(_sessionEcdh.PublicKeyBytes);
        return [.. connectData];
    }

    public AuthResult ProcessPacket(byte[] packetData, object? state)
    {
        return _packetProcessor.ProcessPacket(packetData, state);
    }

    public void SaveState(string serverIp, byte[] requestData)
    {
        _savedState[serverIp] = requestData;
    }

    #region Shared Secret

    private byte[] _sharedSecret = [];
    
    public void SetSharedSecret(byte[] sharedSecret)
    {
        _sharedSecret = sharedSecret;
    }
    
    public byte[] GetSharedSecret()
    {
        return _sharedSecret;
    }
    
    #endregion
}
