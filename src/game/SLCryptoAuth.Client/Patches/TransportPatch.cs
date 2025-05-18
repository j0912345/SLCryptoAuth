using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppLiteNetLib;
using Il2CppLiteNetLib.Utils;
using MelonLoader;
using SLCryptoAuth.Client.Core;

namespace SLCryptoAuth.Client.Patches;

[HarmonyPatch]
internal static class TransportPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(CustomLiteNetLib4MirrorTransport), nameof(CustomLiteNetLib4MirrorTransport.GetConnectData))]
    private static bool GetConnectDataPrefix(NetDataWriter writer)
    {
        var serverIp = CustomNetworkManager.ConnectionIp;
        var connectData = ClientContext.Instance.GetConnectData(serverIp);
        writer.Put(new Il2CppStructArray<byte>(connectData));
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(CustomLiteNetLib4MirrorTransport), nameof(CustomLiteNetLib4MirrorTransport.OnConncetionRefused))]
    private static bool OnConnectionRefusedPrefix(DisconnectInfo disconnectinfo)
    {
        var packetPayload = disconnectinfo.AdditionalData.GetRemainingBytes();
        var authResult = ClientContext.Instance.ProcessPacket(packetPayload, null);

        if (authResult.IsHandled)
        {
            if (authResult.SendAnswer)
            {
                var serverIp = CustomNetworkManager.ConnectionIp;
                ClientContext.Instance.SaveState(serverIp, authResult.Data);
                MelonLogger.Msg("Reconnecting");
                
                CustomNetworkManager.reconnectTime = float.Epsilon;
                CustomNetworkManager.TypedSingleton.Reconnect();
            }

            return false;
        }

        if (authResult.ReplaceData)
            disconnectinfo.AdditionalData.SetSource(authResult.Data);
        
        return true;
    }
}
