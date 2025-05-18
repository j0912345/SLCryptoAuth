using System;
using HarmonyLib;
using LabApi.Loader.Features.Plugins;

namespace SLCryptoAuth.Server;

public class SLCryptoAuthPlugin : Plugin
{
    private const string HarmonyId = "com.alexanderk.sl-crypto-auth";

    private Harmony? _harmonyInstance;

    /// <inheritdoc />
    public override string Name => "SLCryptoAuth";

    /// <inheritdoc />
    public override string Description => "Adds cryptography-based authentication without a central server.";

    /// <inheritdoc />
    public override string Author => "SLCryptoAuth's Team";

    /// <inheritdoc />
    public override Version Version => new(1, 0);

    /// <inheritdoc />
    public override Version RequiredApiVersion => new(1, 0);

    /// <inheritdoc />
    public override void Enable()
    {
        _harmonyInstance = new Harmony(HarmonyId);
        _harmonyInstance.PatchAll();
    }

    /// <inheritdoc />
    public override void Disable()
    {
        _harmonyInstance?.UnpatchAll(HarmonyId);
        _harmonyInstance = null;
    }
}
