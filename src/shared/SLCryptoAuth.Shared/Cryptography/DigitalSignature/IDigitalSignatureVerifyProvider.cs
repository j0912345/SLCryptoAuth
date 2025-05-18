namespace SLCryptoAuth.Cryptography.DigitalSignature;

public interface IDigitalSignatureVerifyProvider
{
    byte[] PublicKeyBytes { get; }

    bool Verify(byte[] data, byte[] signature);
}
