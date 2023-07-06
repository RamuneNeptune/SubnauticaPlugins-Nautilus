

namespace Ramune.PrawnSuitReinforcements
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
            Helpers.CreateUpgrade("PrawnSuitReinforcementMK1", "Prawn suit reinforcement MK1", "A heavily reinforced titanium plating solution.", 100f);
            Helpers.CreateUpgrade("PrawnSuitReinforcementMK2", "Prawn suit reinforcement MK2", "A heavily reinforced titanium plating solution.", 200f);
            Helpers.CreateUpgrade("PrawnSuitReinforcementMK3", "Prawn suit reinforcement MK3", "A heavily reinforced titanium plating solution.", 300f);
            Helpers.CreateUpgrade("PrawnSuitReinforcementMK4", "Prawn suit reinforcement MK4", "A heavily reinforced titanium plating solution.", 400f);

            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}