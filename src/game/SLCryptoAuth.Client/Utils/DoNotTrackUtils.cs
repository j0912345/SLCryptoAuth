using Il2CppUserSettings;

namespace SLCryptoAuth.Client.Utils;

public static class DoNotTrackUtils
{
    public static bool IsEnabled()
    {
        return UserSetting<bool>.Get(25131, 0);
    }
}
