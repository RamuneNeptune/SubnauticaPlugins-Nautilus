
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;

namespace Ramune.IonThermalPlant
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class IonThermalPlant : BaseUnityPlugin
    {
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        private const string myGUID = "com.ramune.IonThermalPlant";
        private const string pluginName = "Ion Thermal Plant";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Items.IonThermalPlant.Patch();
            Items.IonSolarPanel.Patch();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}