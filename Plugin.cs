using BepInEx;
using HarmonyLib;
using RedPill.Scripts;

namespace RedPill
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PLUGIN_GUID = "NBFBs.RedPill";
        private const string PLUGIN_NAME = "Red Pill Enemy Spawn";
        private const string PLUGIN_VERSION = "0.3.0";

        internal static Plugin Instance;

        internal static Harmony harmony;

        private void Awake()
        {
            if (Instance == null) { Instance = this; }

            ModDebug.Initialize(PLUGIN_GUID);
            ModDebug.LogInfo($"Plugin {PLUGIN_GUID} (\"{PLUGIN_NAME}\") is loaded!");

            ConfigController.Initialize(Config);

            harmony = new Harmony(PLUGIN_GUID);

            harmony.PatchAll();
        }
    }
}
