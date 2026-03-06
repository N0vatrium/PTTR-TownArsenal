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


            /*__instance.numBullets = 3;
            __instance.numShootPoints = 3;

            var shootPoint = __instance.shootPoint;
            var shootPoint2 = shootPoint;
            var shootPoint3 = shootPoint;

            var offset = 2;
            shootPoint2.position = new UnityEngine.Vector3(shootPoint.position.x + offset, shootPoint.position.y, shootPoint.position.z);
            shootPoint3.position = new UnityEngine.Vector3(shootPoint.position.x - offset, shootPoint.position.y, shootPoint.position.z);

            var extrapoints = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<UnityEngine.Transform>(2);
            extrapoints[0] = shootPoint2;
            extrapoints[1] = shootPoint3;

            __instance.shootPointExtra = extrapoints;*/

            Helpers.WriteLog("Supercharged weapon " + __instance.name + "(" + __instance.weaponTypeID + ")");
        }
    }
}
