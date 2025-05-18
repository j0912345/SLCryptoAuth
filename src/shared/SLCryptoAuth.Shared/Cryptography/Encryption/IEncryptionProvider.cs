namespace SLCryptoAuth.Cryptography.Encryption;

public interface IEncryptionProvider
{
    byte[] Encrypt(byte[] data);
    byte[] Decrypt(byte[] encryptedData);
}
