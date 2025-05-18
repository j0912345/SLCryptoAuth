using System;
using Il2CppOrg.BouncyCastle.Asn1;
using Il2CppOrg.BouncyCastle.Asn1.Sec;
using Il2CppOrg.BouncyCastle.Asn1.X9;
using Il2CppOrg.BouncyCastle.Crypto;
using Il2CppOrg.BouncyCastle.Crypto.Generators;
using Il2CppOrg.BouncyCastle.Crypto.Parameters;
using Il2CppOrg.BouncyCastle.Math;
using Il2CppOrg.BouncyCastle.Security;

namespace SLCryptoAuth.Cryptography.Utils;

public static class ECKeyManager
{
    #region Curve Parameters and Constants

    public const int PrivateKeyLength = 32;
    public const int PublicKeyLength = 33;

    private static readonly DerObjectIdentifier CurveOId = SecObjectIdentifiers.SecP256r1;
    private static readonly X9ECParameters EcParameters = ECNamedCurveTable.GetByOid(CurveOId);
    private static readonly ECDomainParameters DomainParameters = new(EcParameters);
    private static readonly SecureRandom SecureRandom = new();
    
    #endregion

    #region Key Generation

    public static AsymmetricCipherKeyPair GenerateKeyPair()
    {
        var keyGenerator = new ECKeyPairGenerator();
        var keyGenerationParams = new ECKeyGenerationParameters(DomainParameters, SecureRandom);
        keyGenerator.Init(keyGenerationParams.Cast<KeyGenerationParameters>());
        return keyGenerator.GenerateKeyPair();
    }
    
    #endregion

    #region Private Key Operations

    public static ECPrivateKeyParameters GetPrivateKeyFromBytes(byte[] privateKeyBytes)
    {
        if (privateKeyBytes is null)
            throw new ArgumentNullException(nameof(privateKeyBytes));
        
        if (privateKeyBytes.Length != PrivateKeyLength)
            throw new ArgumentException($"Private key must be {PrivateKeyLength} bytes.", nameof(privateKeyBytes));
        
        var privateKeyD = new BigInteger(1, privateKeyBytes);
        
        if (privateKeyD.SignValue <= 0 || privateKeyD.CompareTo(EcParameters.N) >= 0)
            throw new ArgumentException("Private key scalar is out of valid range.", nameof(privateKeyBytes));
        
        return new ECPrivateKeyParameters(privateKeyD, DomainParameters);
    }

    public static byte[] GetBytesFromPrivateKey(ECPrivateKeyParameters privateKey)
    {
        if (privateKey is null)
            throw new ArgumentNullException(nameof(privateKey));

        return privateKey.D.ToByteArrayUnsigned();
    }

    #endregion

    #region Public Key Operations

    public static ECPublicKeyParameters GetPublicKeyFromBytes(byte[] publicKeyBytes)
    {
        if (publicKeyBytes is null)
            throw new ArgumentNullException(nameof(publicKeyBytes));
        if (publicKeyBytes.Length != PublicKeyLength)
            throw new ArgumentException($"Public key must be {PublicKeyLength} bytes (compressed format).", nameof(publicKeyBytes));
        if (publicKeyBytes[0] != 0x02 && publicKeyBytes[0] != 0x03)
            throw new ArgumentException("Invalid compressed public key format.", nameof(publicKeyBytes));
        
        var publicKeyQ = EcParameters.Curve.DecodePoint(publicKeyBytes);
        return new ECPublicKeyParameters(publicKeyQ, DomainParameters);
    }

    public static byte[] GetBytesFromPublicKey(ECPublicKeyParameters publicKey)
    {
        if (publicKey is null)
            throw new ArgumentNullException(nameof(publicKey));

        return publicKey.Q.GetEncoded(compressed: true);
    }

    public static ECPublicKeyParameters GetPublicKeyFromPrivateKey(ECPrivateKeyParameters privateKey)
    {
        if (privateKey is null)
            throw new ArgumentNullException(nameof(privateKey));

        var publicKeyQ = EcParameters.G.Multiply(privateKey.D).Normalize();
        return new ECPublicKeyParameters(publicKeyQ, DomainParameters);
    }

    #endregion

    #region Key Pair Operations

    public static AsymmetricCipherKeyPair GetKeyPairFromPrivateKeyBytes(byte[] privateKeyBytes)
    {
        var privateKey = GetPrivateKeyFromBytes(privateKeyBytes);
        var publicKey = GetPublicKeyFromPrivateKey(privateKey);
        return new AsymmetricCipherKeyPair(publicKey, privateKey);
    }

    public static (byte[] privateKeyBytes, byte[] publicKeyBytes) GetBytesFromKeyPair(AsymmetricCipherKeyPair keyPair)
    {
        if (keyPair is null)
            throw new ArgumentNullException(nameof(keyPair));

        var privateKey = keyPair.Private.Cast<ECPrivateKeyParameters>();
        var publicKey = keyPair.Public.Cast<ECPublicKeyParameters>();
        
        var privateKeyBytes = GetBytesFromPrivateKey(privateKey);
        var publicKeyBytes = GetBytesFromPublicKey(publicKey);
        
        return (privateKeyBytes, publicKeyBytes);
    }

    #endregion
}
