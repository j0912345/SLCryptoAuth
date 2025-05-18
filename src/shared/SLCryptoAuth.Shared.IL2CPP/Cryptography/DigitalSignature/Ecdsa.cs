using System;
using Il2CppOrg.BouncyCastle.Crypto;
using Il2CppOrg.BouncyCastle.Security;
using SLCryptoAuth.Cryptography.Utils;

namespace SLCryptoAuth.Cryptography.DigitalSignature;

public class Ecdsa : IDigitalSignatureProvider
{
    private const string Algorithm = "SHA-256withECDSA";
    
    private readonly AsymmetricCipherKeyPair _keyPair;

    public Ecdsa()
    {
        _keyPair = ECKeyManager.GenerateKeyPair();
        (PrivateKeyBytes, PublicKeyBytes) = ECKeyManager.GetBytesFromKeyPair(_keyPair);
    }

    public Ecdsa(byte[] privateKeyBytes)
    {
        _keyPair = ECKeyManager.GetKeyPairFromPrivateKeyBytes(privateKeyBytes);
        (PrivateKeyBytes, PublicKeyBytes) = ECKeyManager.GetBytesFromKeyPair(_keyPair);
    }

    /// <inheritdoc />
    public byte[] PrivateKeyBytes { get; }

    /// <inheritdoc />
    public byte[] PublicKeyBytes { get; }

    /// <inheritdoc />
    public byte[] Sign(byte[] data)
    {
        var signer = SignerUtilities.GetSigner(Algorithm);
        signer.Init(forSigning: true, _keyPair.Private.Cast<ICipherParameters>());
        signer.BlockUpdate(data, 0, data.Length);
        return signer.GenerateSignature();
    }
    
    /// <inheritdoc />
    public bool Verify(byte[] data, byte[] signature)
    {
        var signer = SignerUtilities.GetSigner(Algorithm);
        signer.Init(forSigning: false, _keyPair.Public.Cast<ICipherParameters>());
        signer.BlockUpdate(data, 0, data.Length);
        return signer.VerifySignature(signature);
    }
}
