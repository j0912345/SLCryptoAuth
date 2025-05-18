using System;

namespace SLCryptoAuth.IO.Config.Client;

public class TrustedServerEntry
{
    public string Ip { get; set; }
    public string Fingerprint { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public string Description { get; set; }
}