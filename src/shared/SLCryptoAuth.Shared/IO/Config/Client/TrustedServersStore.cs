using System.Collections.Generic;

namespace SLCryptoAuth.IO.Config.Client;

public class TrustedServersStore
{
    private readonly List<TrustedServerEntry> _trustedServers;

    public TrustedServersStore(List<TrustedServerEntry> trustedServers)
    {
        _trustedServers = trustedServers;
    }
}