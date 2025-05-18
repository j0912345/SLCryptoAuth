using HarmonyLib;
using Il2CppCentralAuth;
using SLCryptoAuth.Client.Core;

namespace SLCryptoAuth.Client.Patches;

[HarmonyPatch]
internal static class AuthManagerPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerAuthenticationManager), nameof(PlayerAuthenticationManager.Start))]
    private static void StartPostfix(PlayerAuthenticationManager __instance)
    {
        if (!__instance.isLocalPlayer) return;
        __instance._hub.encryptedChannelManager.EncryptionKey = ClientContext.Instance.GetSharedSecret();
    }
}
