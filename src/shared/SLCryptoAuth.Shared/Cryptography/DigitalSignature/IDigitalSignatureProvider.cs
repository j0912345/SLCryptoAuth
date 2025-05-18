namespace SLCryptoAuth.Cryptography.DigitalSignature;

public interface IDigitalSignatureProvider : IDigitalSignatureVerifyProvider
{
    byte[] PrivateKeyBytes { get; }

    byte[] Sign(byte[] data);
}
