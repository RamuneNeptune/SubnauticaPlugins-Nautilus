
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;

namespace Ramune.IonNuclearReactor
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class IonNuclearReactor : BaseUnityPlugin
    {
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        private const string myGUID = "com.ramune.IonNuclearReactor";
        private const string pluginName = "Ion Nuclear Reactor";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Items.IonNuclearReactor.Patch();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}