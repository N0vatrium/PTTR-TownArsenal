using HarmonyLib;


namespace TownArsenal.Patches
{
    [HarmonyPatch(typeof(WeaponGun), "OnEnable")]
    internal static class Patch_weapon
    {
        private static bool Enabled = PluginConfig.ConfigGunEnabled.Value && !NetworkManager.IsMultiplayer;

        static void Postfix(WeaponGun __instance)
        {
            if (!__instance.isGun || !Enabled)
            {
                return;
            }

            __instance.unlimitedAmmo = PluginConfig.ConfigUnlimitedAmmo.Value;
            __instance.shotFrequency *= PluginConfig.ConfigCdr.Value;
            Helpers.WriteLog("Supercharged weapon " + __instance.name + "(" + __instance.weaponTypeID + ")");
        }
    }
}
