
using static Ramune.RamunesOutcrops.Resources;
using BepInEx.Logging;
using HarmonyLib;
using RamuneLib;
using BepInEx;
using UnityEngine;
using Ramune.RamunesOutcrops.Items;
using Ramune.RamunesOutcrops.Fabricators;

namespace Ramune.RamunesOutcrops
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class RamunesOutcrops : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.RamunesOutcrops";
        private const string pluginName = "Ramunes Outcrops";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            logger = Logger;
            harmony.PatchAll();
            Main.FindPiracy();

            Fabricators.RadiantFabricator.Patch();
            Items.RadiantCrystal.Patch();
            Items.RadiantThermoblade.Patch();
            Items.RadiantFins.Patch();
            Items.RadiantCube.Patch();
            Items.RadiantTank.Patch();
            Items.RadiantSeaglide.Patch();
            Items.RadiantRebreather.Patch();

            logger.LogDebug("'LodestoneOutcrop' is being created with set BiomeData..");
            Helpers.CreateOutcrop("LodestoneOutcrop", "Lodestone outcrop", "A lodestone outcrop.", TechType.ShaleChunk, Helpers.CreateBiomeData(LodestoneOutcrop));

            logger.LogDebug("'GeyseriteOutcrop' is being created with set BiomeData..");
            Helpers.CreateOutcrop("GeyseriteOutcrop", "Geyserite outcrop", "A geyserite outcrop.", TechType.ShaleChunk, Helpers.CreateBiomeData(GeyseriteOutcrop));

            logger.LogDebug("'SiltstoneOutcrop' is being created with set BiomeData..");
            Helpers.CreateOutcrop("SiltstoneOutcrop", "Siltstone outcrop", "A siltstone outcrop.", TechType.LimestoneChunk, Helpers.CreateBiomeData(SiltstoneOutcrop));

            logger.LogDebug("'SerpentiteOutcrop' is being created with set BiomeData..");
            Helpers.CreateOutcrop("SerpentiteOutcrop", "Serpentite outcrop", "A serpentite outcrop.", TechType.SandstoneChunk, Helpers.CreateBiomeData(SerpentiniteOutcrop));            
            
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
        }
    }
}