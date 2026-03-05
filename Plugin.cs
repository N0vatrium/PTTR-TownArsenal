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

    public override void Load()
    {
        // Plugin startup logic
        Log = base.Log;
        PluginConfig.Init(Config);

        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

        harmony.PatchAll(Assembly.GetExecutingAssembly());

        Helpers.WriteLog($"Plugin loaded, gun module is " + PluginConfig.ConfigGunEnabled.Value + " and melee module is " + PluginConfig.ConfigMeleeEnabled.Value);
    }

}
