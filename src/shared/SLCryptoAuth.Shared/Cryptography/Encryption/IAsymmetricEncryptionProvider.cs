namespace SLCryptoAuth.Cryptography.Encryption;

public interface IAsymmetricEncryptionProvider : IEncryptionProvider
{
    byte[] PrivateKeyBytes { get; }
    byte[] PublicKeyBytes { get; }
}
