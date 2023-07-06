
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;

namespace Ramune.PrawnSuitTurboModule
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class ExosuitTurbo : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.PrawnSuitTurboModule";
        private const string pluginName = "Prawn Suit Turbo Module";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Items.PrawnSuitTurboUpgrade.Patch();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}