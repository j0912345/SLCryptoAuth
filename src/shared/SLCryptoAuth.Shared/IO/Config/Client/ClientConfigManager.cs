using System;
using System.IO;

namespace SLCryptoAuth.IO.Config.Client;

public class ClientConfigManager
{
    //private static readonly string SLCryptoAuthDirectoryPath = Path.Combine(FileManager._appfolder, "SLCryptoAuth");
    
    public TrustedServersStore TrustedServersStore { get; private set; }
    
    
}