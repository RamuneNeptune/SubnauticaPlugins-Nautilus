
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using Nautilus.Handlers;
using Nautilus.Options.Attributes;
using Nautilus.Json;
using RamuneLib;
using Nautilus.Options;

namespace Ramune.EarlyIonBattery
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class EarlyIonBattery : BaseUnityPlugin
    {
        internal static Options config { get; } = OptionsPanelHandler.RegisterModOptions<Options>();

        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.EarlyIonBattery";
        private const string pluginName = "Early Ion Battery";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
    [Menu("Early Ion Battery")]
    public class Options : ConfigFile
    {
        [Choice("Ion battery unlocks with:", new[] { "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal", "<color=#ffcf3c><b>2/3 </b></color> Disease Research Facility", "<color=#ffcf3c><b>3/3 </b></color> Lost River Cache Terminal" })]
        public string unlockBatt = "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal";

        [Choice("Ion power cell unlocks with:", new[] { "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal", "<color=#ffcf3c><b>2/3 </b></color> Disease Research Facility", "<color=#ffcf3c><b>3/3 </b></color> Lost River Cache Terminal" })]
        public string unlockCell = "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal";
        
        [Button("Force <color=#ff4200>un-learn</color> Ion battery & power cell", Tooltip = "Click to un-learn the Ion battery & power cell blueprints")]
        public void Unlearn(ButtonClickedEventArgs _)
        {
            if(!Player.main) return;
            KnownTech.Remove(TechType.PrecursorIonBattery);
            KnownTech.Remove(TechType.PrecursorIonPowerCell);
            ErrorMessage.AddError("<color=#ff4200>Removed </color>'Ion Battery'<color=#ff3417> & </color>'Ion power cell'<color=#ff3417> from KnownTech</color>");
        }

        [Button("Force <color=#9ae86e>learn</color> Ion battery & power cell", Tooltip = "Click to learn the Ion battery & power cell blueprints")]
        public void Learn(ButtonClickedEventArgs _)
        {
            if(!Player.main) return;
            KnownTech.Add(TechType.PrecursorIonBattery);
            KnownTech.Add(TechType.PrecursorIonPowerCell);
            ErrorMessage.AddError("<color=#9ae86e>Added </color>'Ion battery'<color=#9ae86e> & </color>'Ion power cell'<color=#9ae86e> to KnownTech</color>");
        }
    }
}