using HarmonyLib;


namespace TownArsenal.Patches
{
    [HarmonyPatch(typeof(WeaponGun), "OnEnable")]
    internal static class Patch_weapon
    {
        static void Postfix(WeaponGun __instance)
        {
            if (NetworkManager.IsMultiplayer)
            {
                Helpers.WriteLog("Mod disabled in multiplayer", error: true);
                return;
            }


            if (!__instance.isGun)
            {
                return;
            }

            __instance.unlimitedAmmo = Plugin.configUnlimitedAmmo.Value;
            __instance.shotFrequency *= Plugin.configCdr.Value;
            Helpers.WriteLog("Supercharged weapon " + __instance.name + "(" + __instance.weaponTypeID + ")");
        }
    }
}
