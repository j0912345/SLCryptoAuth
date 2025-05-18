using UnityEngine;

namespace SLCryptoAuth.Shared;

public static class NicknameUtils
{
    private const string RegistryNicknameKey = "nickname";

    public static string GetNickname()
    {
        if (PlayerPrefs.HasKey(RegistryNicknameKey))
            return PlayerPrefs.GetString(RegistryNicknameKey);

        var defaultNickname = GetDefaultNickname();
        SetNickname(defaultNickname);
        return defaultNickname;
    }

    public static void SetNickname(string newNickname)
    {
        PlayerPrefs.SetString(RegistryNicknameKey, newNickname);
    }

    public static void ResetNickname()
    {
        var defaultNickname = GetDefaultNickname();
        SetNickname(defaultNickname);
    }

    private static string GetDefaultNickname()
    {
        return $"Player {SystemInfo.deviceName}";
    }
}