
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;

namespace Ramune.IonSolarPanel
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class IonSolarPanel : BaseUnityPlugin
    {
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        private const string myGUID = "com.ramune.IonSolarPanel";
        private const string pluginName = "Ion Solar Panel";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Items.IonCrystalShard.Patch();
            Items.IonSolarPanelKit.Patch();
            Items.IonSolarPanel.Patch();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}