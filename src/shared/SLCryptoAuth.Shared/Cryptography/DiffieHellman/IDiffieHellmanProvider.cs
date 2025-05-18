namespace SLCryptoAuth.Cryptography.DiffieHellman;

public interface IDiffieHellmanProvider
{
    byte[] PrivateKeyBytes { get; }
    byte[] PublicKeyBytes { get; }
    
    byte[] ComputeSharedSecret(byte[] otherPublicKeyBytes);
}
