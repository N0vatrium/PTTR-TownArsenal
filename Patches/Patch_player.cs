using HarmonyLib;

namespace TownArsenal.Patches
{
    [HarmonyPatch(typeof(PTTRPlayer), "EnemyKilled")]
    internal static class Patch_player
    {
        private static float SpeedIncrement;
        private static float SpeedMaximum;

        private static float AtkSpeedIncrement;
        private static float AtkSpeedMaximum;

        static Patch_player()
        {
            SpeedIncrement = PluginConfig.ConfigMeleeSpeed.Value / 10;
            SpeedMaximum = PluginConfig.ConfigMeleeSpeedMaximum.Value;

            AtkSpeedIncrement = PluginConfig.ConfigMeleeAtkSpeed.Value / 10;
            AtkSpeedMaximum = PluginConfig.ConfigMeleeAtkSpeedMaximum.Value;
        }

        private static bool Enabled = PluginConfig.ConfigMeleeEnabled.Value && !NetworkManager.IsMultiplayer;
        static void Prefix(PTTRPlayer __instance)
        {
            if (__instance.HasWeapon() && __instance.CurrentWeapon().isGun || !Enabled)
            {
                return;
            }

            var baseAtk = __instance.playerClassObj.attackSpeedMultiplier;
            baseAtk += AtkSpeedIncrement;

            if (baseAtk <= AtkSpeedMaximum)
            {
                __instance.playerClassObj.attackSpeedMultiplier = baseAtk;
            }

            var baseSpeed = __instance.playerClassObj.speedMultiplier;
            baseSpeed += SpeedIncrement;

            if (baseSpeed <= SpeedMaximum)
            {
                __instance.playerClassObj.speedMultiplier = baseSpeed;
            }

        }
    }
}
