using HarmonyLib;


#nullable enable
namespace TownArsenal.Patches
{
    [HarmonyPatch(typeof(PTTRPlayer), "EnemyKilled")]
    internal static class Patch_player
    {
        private static float SpeedIncrement;
        private static float SpeedMaximum;

        private static float AtkSpeedIncrement;
        private static float AtkSpeedMaximum;

        public static float? BaseSpeed = null;
        public static float? BaseAtkSpeed = null;
        public static PTTRPlayer.Class? TargetClass = null;

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

            TargetClass ??= __instance.playerClassObj;
            BaseSpeed ??= __instance.playerClassObj.speedMultiplier;
            BaseAtkSpeed ??= __instance.playerClassObj.attackSpeedMultiplier;

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

    [HarmonyPatch(typeof(LoadingScreen), "Awake")]
    internal static class Patch_player_reset
    {
        static void Prefix()
        {
            if (Patch_player.TargetClass == null)
            {
                return;
            }

            if (Patch_player.BaseSpeed != null)
            {
                Patch_player.TargetClass.speedMultiplier = (float)Patch_player.BaseSpeed;
                Patch_player.BaseSpeed = null;
            }

            if (Patch_player.BaseAtkSpeed != null)
            {
                Patch_player.TargetClass.attackSpeedMultiplier = (float)Patch_player.BaseAtkSpeed;
                Patch_player.BaseAtkSpeed = null;
            }

        }
    }
}
