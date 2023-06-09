
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using Nautilus.Handlers;
using RamuneLib;
using static CraftData;

namespace Ramune.BZTitaniumIngot
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class SeaglideUpgrades : BaseUnityPlugin
    {
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        private const string myGUID = "com.ramune.BZTitaniumIngot";
        private const string pluginName = "BZ Titanium Ingot";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            CraftDataHandler.SetRecipeData(TechType.TitaniumIngot, Utilities.CreateRecipe(1, new Ingredient(TechType.Titanium, 5)));
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}