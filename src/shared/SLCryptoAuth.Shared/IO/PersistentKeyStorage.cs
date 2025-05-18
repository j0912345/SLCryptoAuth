using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace SLCryptoAuth.IO;

public class PersistentKeyStorage(string filePath)
{
    public bool TryGetIdentityPrivateKey([NotNullWhen(true)] out byte[]? identityPrivateKey)
    {
        if (!File.Exists(filePath))
        {
            identityPrivateKey = null;
            return false;
        }
        
        
        identityPrivateKey = File.ReadAllBytes(filePath);
        return true;
    }

    public void SaveIdentityPrivateKey(byte[] identityPrivateKey)
    {
        File.WriteAllBytes(filePath, identityPrivateKey);
    }
}
