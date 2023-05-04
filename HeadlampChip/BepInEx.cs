
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib.Main;

namespace Ramune.HeadlampChip
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class HeadlampChip : BaseUnityPlugin
    {
        private const string myGUID = "com.ramune.HeadlampChip";
        private const string pluginName = "Headlamp Chip";
        private const string versionString = "1.0.0";
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        public void Awake()
        {
            harmony.PatchAll();
            Checks.FindPiracy();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}