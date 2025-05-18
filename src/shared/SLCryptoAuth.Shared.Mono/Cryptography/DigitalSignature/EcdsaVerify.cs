using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using SLCryptoAuth.Cryptography.Utils;

namespace SLCryptoAuth.Cryptography.DigitalSignature;

public class EcdsaVerify : IDigitalSignatureVerifyProvider
{
    private const string Algorithm = "SHA-256withECDSA";
    
    private readonly ECPublicKeyParameters _publicKey;

    public EcdsaVerify(ECPublicKeyParameters publicKey)
    {
        _publicKey = publicKey;
        PublicKeyBytes = ECKeyManager.GetBytesFromPublicKey(_publicKey);
    }

    public EcdsaVerify(byte[] publicKeyBytes)
    {
        if (publicKeyBytes.Length < ECKeyManager.PublicKeyLength)
            throw new ArgumentException("Public key length is less than expected.");

        _publicKey = ECKeyManager.GetPublicKeyFromBytes(publicKeyBytes);
        PublicKeyBytes = ECKeyManager.GetBytesFromPublicKey(_publicKey);
    }

    /// <inheritdoc />
    public byte[] PublicKeyBytes { get; }

    /// <inheritdoc />
    public bool Verify(byte[] data, byte[] signature)
    {
        var signer = SignerUtilities.GetSigner(Algorithm);
        signer.Init(forSigning: false, _publicKey);
        signer.BlockUpdate(data, 0, data.Length);
        return signer.VerifySignature(signature);
    }
}
