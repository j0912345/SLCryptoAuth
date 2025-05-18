using System;
using Il2CppOrg.BouncyCastle.Crypto;
using Il2CppOrg.BouncyCastle.Crypto.Parameters;
using Il2CppOrg.BouncyCastle.Security;

namespace SLCryptoAuth.Cryptography.Encryption;

public class Aes : ISymmetricEncryptionProvider
{
    private const string Algorithm = "AES/CBC/PKCS7Padding";

    private static readonly SecureRandom SecureRandom = new();
    
    private readonly KeyParameter _keyParameter;
    
    public Aes()
    {
        var keyBytes = new byte[32]; // 256 bits
        SecureRandom.NextBytes(keyBytes);
        KeyBytes = keyBytes;
        _keyParameter = ParameterUtilities.CreateKeyParameter("AES", keyBytes, 0, keyBytes.Length);
    }

    public Aes(byte[] keyBytes)
    {
        KeyBytes = keyBytes;
        _keyParameter = ParameterUtilities.CreateKeyParameter("AES", keyBytes, 0, keyBytes.Length);
    }

    public byte[] KeyBytes { get; }

    public byte[] Encrypt(byte[] data)
    {
        var iv = new byte[16];
        new SecureRandom().NextBytes(iv);
        
        var cipher = CipherUtilities.GetCipher(Algorithm);
        var keyParamWithIv = new ParametersWithIV(_keyParameter.Cast<ICipherParameters>(), iv);
        cipher.Init(true, keyParamWithIv.Cast<ICipherParameters>());
        
        var encryptedBytes = cipher.DoFinal(data);
        var result = new byte[iv.Length + encryptedBytes.Length];
        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);
        
        return result;
    }

    public byte[] Decrypt(byte[] encryptedData)
    {
        var iv = new byte[16];
        Buffer.BlockCopy(encryptedData, 0, iv, 0, iv.Length);
        
        var encryptedBytes = new byte[encryptedData.Length - iv.Length];
        Buffer.BlockCopy(encryptedData, iv.Length, encryptedBytes, 0, encryptedBytes.Length);
        
        var cipher = CipherUtilities.GetCipher(Algorithm);
        var keyParamWithIv = new ParametersWithIV(_keyParameter.Cast<ICipherParameters>(), iv);
        cipher.Init(false, keyParamWithIv.Cast<ICipherParameters>());

        return cipher.DoFinal(encryptedBytes);
    }
}
