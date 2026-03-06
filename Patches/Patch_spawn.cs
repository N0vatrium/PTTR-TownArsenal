using HarmonyLib;
using TownArsenal;

namespace ClassPicker.Patches
{
    [HarmonyPatch(typeof(PTTRPlayer), "Awake")]
    internal static class Patch_spawn
    {
        private static bool Enabled = PluginConfig.ConfigBigKatanaEnabled.Value && !NetworkManager.IsMultiplayer;

        static void Postfix(PTTRPlayer __instance)
        {
            if (!Enabled || GameManager.IsUGC)
            {
                return;
            }

            var katana = Helpers.SpawnItemAt(Helpers.GUID_BIG_KATANA, __instance.transform.position);
            katana.GetComponent<Weapon>().unbreakable = true;
        }
    }

    [HarmonyPatch(typeof(PlayerStartPositions), "SetToPosition", [typeof(PTTRPlayer), typeof(int)])]
    internal static class Patch_spawn_ugc
    {
        private static bool Enabled = PluginConfig.ConfigBigKatanaEnabled.Value && !NetworkManager.IsMultiplayer;

        static void Postfix(PTTRPlayer player, int positionIndex)
        {
            if (!Enabled || !GameManager.IsUGC)
            {
                return;
            }

            var katana = Helpers.SpawnItemAt(Helpers.GUID_BIG_KATANA, player.transform.position);
            katana.GetComponent<Weapon>().unbreakable = true;
        }
    }
}
