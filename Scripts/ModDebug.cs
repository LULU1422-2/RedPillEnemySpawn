using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedPill.Scripts
{
    internal static class ModDebug
    {
        private static ManualLogSource manualLogSource;

        internal static void Initialize(string GUID)
        {
            manualLogSource = Logger.CreateLogSource(GUID);
        }

        internal static void LogInfo(string message)
        {
#if DEBUG
            manualLogSource.LogInfo(message);
#endif
        }

        internal static void LogWarning(string message)
        {
#if DEBUG
            manualLogSource.LogWarning(message);
#endif
        }

        internal static void LogError(string message)
        {
#if DEBUG
            manualLogSource.LogError(message);
#endif
        }
    }
}
