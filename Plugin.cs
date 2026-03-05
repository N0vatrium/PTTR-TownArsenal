using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Reflection;

namespace TownArsenal;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    internal static ConfigEntry<bool> configUnlimitedAmmo;
    internal static ConfigEntry<float> configCdr;

    public override void Load()
    {
        // Plugin startup logic
        Log = base.Log;

        configUnlimitedAmmo = Config.Bind("General", "UnlimitedAmmo", true, "Set unlimited ammo for all weapons, true/false");
        configCdr = Config.Bind("General", "CooldownPercent", 0.4f, "Set weapon cooldown (ex 0.4 = 40% of initial cd OR 60% faster), 0-1");

        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

        harmony.PatchAll(Assembly.GetExecutingAssembly());

        Helpers.WriteLog($"Plugin loaded, unlimited ammo is " + configUnlimitedAmmo.Value + " and final cd is " + configCdr.Value);
    }

}
