using System;
using System.Security.Cryptography;
using SLCryptoAuth.Cryptography.DiffieHellman;
using SLCryptoAuth.Cryptography.DigitalSignature;

namespace SLCryptoAuth.Server.Core;

public class PreAuthRecord
{
    public PreAuthRecord(byte[] clientSessionPublicKey)
    {
        ServerSession = new Ecdh();
        SharedSecret = ServerSession.ComputeSharedSecret(clientSessionPublicKey);
        Added = DateTime.UtcNow.Ticks;
    }

    public Ecdh ServerSession { get; }
    public byte[] SharedSecret { get; }
    public long Added { get; }

    public EcdsaVerify? ClientIdentity { get; private set; }
    public string? UserId { get; private set; }
    public bool? DoNotTrack { get; private set; }
    public string? Nickname { get; private set; }

    internal void SetClientIdentity(byte[] clientIdentityPublicKey)
    {
        ClientIdentity = new EcdsaVerify(clientIdentityPublicKey);
        var fingerprint = GetFingerprint(clientIdentityPublicKey);
        UserId = string.Concat(fingerprint, "@crypto");
    }

    internal void SetDoNotTrack(bool value)
    {
        DoNotTrack = value;
    }

    internal void SetNickname(string value)
    {
        Nickname = value;
    }

    private static string GetFingerprint(byte[] publicKey)
    {
        using var sha256Hash = SHA256.Create();
        var identityFingerprintBytes = sha256Hash.ComputeHash(publicKey);
        return BitConverter.ToString(identityFingerprintBytes).Replace("-", "").ToUpper();
    }
}
