using HarmonyLib;

namespace TownArsenal.Patches
{
    [HarmonyPatch(typeof(Enemy), "DoAttackDamage")]
    internal static class Patch_enemy
    {
        private static float SpeedIncrement;
        private static float SpeedMaximum;

        private static float AtkSpeedIncrement;
        private static float AtkSpeedMaximum;

        private static bool Enabled = PluginConfig.ConfigMeleeEnabled.Value && !NetworkManager.IsMultiplayer;

        static Patch_enemy()
        {
            SpeedIncrement = PluginConfig.ConfigMeleeSpeed.Value;
            SpeedMaximum = PluginConfig.ConfigMeleeSpeedMaximum.Value;

            AtkSpeedIncrement = PluginConfig.ConfigMeleeAtkSpeed.Value;
            AtkSpeedMaximum = PluginConfig.ConfigMeleeAtkSpeedMaximum.Value;
        }

        static void Prefix(Enemy __instance, ref float damage)
        {
            if(__instance.hasWeapon && __instance.weapon.isGun || !Enabled)
            {
                return;
            }

            var baseAtk = __instance.attackSpeedMultiplier;
            baseAtk += AtkSpeedIncrement;

            if (baseAtk <= AtkSpeedMaximum)
            {
                __instance.attackSpeedMultiplier = baseAtk;
            }

            var baseSpeed = __instance.speedMod;
            baseSpeed += SpeedIncrement;

            if (baseSpeed <= SpeedMaximum)
            {
                __instance.speedMod = baseSpeed;
                __instance.combatSpeedMod = baseSpeed;                
            }
        }
    }
}
