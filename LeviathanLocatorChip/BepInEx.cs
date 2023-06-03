
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using Nautilus.Handlers;
using UnityEngine;

namespace Ramune.LeviathanLocatorChip
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class LeviathanLocatorChip : BaseUnityPlugin
    {
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        private const string myGUID = "com.ramune.LeviathanLocatorChip";
        private const string pluginName = "Leviathan Locator Chip";
        private const string versionString = "1.0.0";

        public static PingType Dragon;
        public static PingType Ghost;
        public static PingType GhostJuvenile;
        public static PingType Reaper;
        public static PingType Treader;

        public static Color[] colors = new Color[]
        {
            new Color32(73, 190, 255, 255), // 1. Default blue
            new Color32(255, 146, 71, 255), // 2. Default orange
            new Color32(219, 95, 64, 255),  // 3. Default red
            new Color32(93, 205, 200, 255), // 4. Default cyan
            new Color32(255, 209, 0, 255),  // 5. Default yellow

            new Color32(192, 52, 57, 255),   // 6. Reaper Leviathan
            new Color32(31, 188, 255, 255),  // 7. Ghost Leviathan
            new Color32(190, 133, 48, 255),  // 8. Sea Treader
            new Color32(52, 170, 53, 255)    // 9. Sea Dragon
        };

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Items.LeviathanLocatorChip.Patch();

            Dragon = EnumHandler.AddEntry<PingType>("Dragon").WithIcon(Utilities.GetSprite("Dragon"));
            Ghost = EnumHandler.AddEntry<PingType>("Ghost").WithIcon(Utilities.GetSprite("Ghost"));
            GhostJuvenile = EnumHandler.AddEntry<PingType>("GhostJuvenile").WithIcon(Utilities.GetSprite("GhostJuvenile"));
            Reaper = EnumHandler.AddEntry<PingType>("Reaper").WithIcon(Utilities.GetSprite("Reaper"));
            Treader = EnumHandler.AddEntry<PingType>("Treader").WithIcon(Utilities.GetSprite("Treader"));

            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}