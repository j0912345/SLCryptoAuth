namespace SLCryptoAuth.Cryptography.Encryption;

public interface ISymmetricEncryptionProvider : IEncryptionProvider
{
    byte[] KeyBytes { get; }
}
