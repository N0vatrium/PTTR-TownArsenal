using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

#nullable enable
namespace TownArsenal
{
    internal static class Helpers
    {
        public static string GUID_BIG_KATANA = "9766a4851c58f484d97fcacf8df71c46";

        public static void WriteLog(object log, bool warning = false, bool error = false)
        {
            var level = BepInEx.Logging.LogLevel.Message;
            if (warning)
            {
                level = BepInEx.Logging.LogLevel.Warning;
            }

            if (error)
            {
                level = BepInEx.Logging.LogLevel.Error;
            }

            Plugin.Log.Log(level, log);
        }

        public static GameObject? SpawnItemAt(string guid, Vector3 position)
        {
            var asset = LoadAsset(guid);

            if (asset == null)
            {
                return null;
            }

            var child = GameObject.Instantiate(asset);
            child.transform.position = position;

            return child;
        }

        public static GameObject? LoadAsset(string guid)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(guid);
            handle.WaitForCompletion();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                WriteLog("LOADED " + handle.Result.name);

                return handle.Result;
            }
            else
            {
                WriteLog("LOADED failed GUID " + guid, error: true);

                return null;
            }
        }
    }
}
