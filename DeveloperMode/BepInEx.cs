
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;

namespace Ramune.DeveloperMode
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class DeveloperMode : BaseUnityPlugin
    {
        private const string myGUID = "com.ramune.DeveloperMode";
        private const string pluginName = "Developer Mode";
        private const string versionString = "1.0.1";

        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}