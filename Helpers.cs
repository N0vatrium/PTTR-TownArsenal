using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownArsenal
{
    internal static class Helpers
    {
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
    }
}
