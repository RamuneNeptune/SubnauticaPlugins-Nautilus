
using static Ramune.RamunesOutcrops.Resources;
using BepInEx.Logging;
using HarmonyLib;
using RamuneLib;
using BepInEx;


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

        public static TechType LodestoneOutcrop;
        public static TechType GeyseriteOutcrop;
        public static TechType SiltstoneOutcrop;
        public static TechType SerpentiteOutcrop;

        public void Awake()
        {
            logger = Logger;
            harmony.PatchAll();
            Main.FindPiracy();

            Fabricators.RadiantFabricator.Patch();
            Constructables.RadiantLocker.Patch();
            Items.RadiantCrystal.Patch();
            Items.RadiantThermoblade.Patch();
            Items.RadiantFins.Patch();
            Items.RadiantCube.Patch();
            Items.RadiantTank.Patch();
            Items.RadiantSeaglide.Patch();
            Items.RadiantRebreather.Patch();

            LodestoneOutcrop = Helpers.CreateOutcrop("LodestoneOutcrop", "Lodestone outcrop", "A lodestone outcrop.", TechType.ShaleChunk, Helpers.CreateBiomeData(LodestoneOutcropBiomes), LodestoneOutcropItems);
            GeyseriteOutcrop = Helpers.CreateOutcrop("GeyseriteOutcrop", "Geyserite outcrop", "A geyserite outcrop.", TechType.ShaleChunk, Helpers.CreateBiomeData(GeyseriteOutcropBiomes), GeyseriteOutcropItems);
            SiltstoneOutcrop = Helpers.CreateOutcrop("SiltstoneOutcrop", "Siltstone outcrop", "A siltstone outcrop.", TechType.LimestoneChunk, Helpers.CreateBiomeData(SiltstoneOutcropBiomes), SiltstoneOutcropItems);
            SerpentiteOutcrop = Helpers.CreateOutcrop("SerpentiteOutcrop", "Serpentite outcrop", "A serpentite outcrop.", TechType.SandstoneChunk, Helpers.CreateBiomeData(SerpentiteOutcropBiomes), SerpentiteOutcropItems);

            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
        }
    }
}