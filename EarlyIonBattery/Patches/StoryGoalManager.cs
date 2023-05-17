
using HarmonyLib;
using RamuneLib;
using Story;
using Main = Ramune.EarlyIonBattery.EarlyIonBattery;

namespace Ramune.EarlyIonBattery.Patches
{
    [HarmonyPatch(typeof(StoryGoalManager), "OnGoalComplete")]
    public static class StoryGoalManager_OnGoalComplete_Patch
    {
        public static string battery;
        public static string powercell;
        public const string opt1 = "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal";
        public const string opt2 = "<color=#ffcf3c><b>2/3 </b></color> Disease Research Facility";
        public const string opt3 = "<color=#ffcf3c><b>3/3 </b></color> Lost River Cache Terminal";

        public static void Postfix(StoryGoalManager __instance)
        {
            if(KnownTech.Contains(TechType.PrecursorIonBattery) || KnownTech.Contains(TechType.PrecursorIonPowerCell)) return;

            switch(Main.config.unlockBatt) {
                case opt1:
                    battery = "Precursor_Gun_DataDownload1";
                    Utilities.Log(Colors.Green, "1");
                    break;
                case opt2:
                    battery = "FindPrecursorLostRiverFacility";
                    Utilities.Log(Colors.Green, "2");
                    break;
                case opt3:
                    battery = "Precursor_Cache_DataDownloadLostRiver";
                    Utilities.Log(Colors.Green, "3");
                    break;
            }

            switch(Main.config.unlockCell)
            {
                case opt1:
                    powercell = "Precursor_Gun_DataDownload1";
                    Utilities.Log(Colors.Lemon, "1");
                    break;
                case opt2:
                    powercell = "FindPrecursorLostRiverFacility";
                    Utilities.Log(Colors.Lemon, "2");
                    break;
                case opt3:
                    powercell = "Precursor_Cache_DataDownloadLostRiver";
                    Utilities.Log(Colors.Lemon, "3");
                    break;
            }

            if(__instance.completedGoals.Contains(battery))
            {
                KnownTech.Add(TechType.PrecursorIonBattery, true);
                ErrorMessage.AddError("<color=#09f88a>Unlocked:</color> Ion battery");
            }
            if(__instance.completedGoals.Contains(powercell))
            {
                KnownTech.Add(TechType.PrecursorIonPowerCell, true);
                ErrorMessage.AddError("<color=#09f88a>Unlocked:</color> Ion power cell");
            }
        }
    }
}