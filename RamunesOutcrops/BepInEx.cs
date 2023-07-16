

namespace Ramune.RamunesOutcrops
{
    [BepInDependency("com.snmodding.nautilus")]
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
            harmony.PatchAll();
            Main.FindPiracy();
            this.Init();
            logger = Logger;
        }

        public void Init()
        {
            Logger.LogInfo($"{pluginName} {versionString} is patching outcrops..");

            LodestoneOutcrop = Helpers.CreateOutcrop("LodestoneOutcrop",  "Lodestone outcrop",  "A lodestone outcrop.",  TechType.BasaltChunk, Helpers.CreateBiomeData(Data.BiomeData.Lodestone), Data.OutcropData.Lodestone);
            GeyseriteOutcrop = Helpers.CreateOutcrop("GeyseriteOutcrop",  "Geyserite outcrop",   "A geyserite outcrop.", TechType.ShaleChunk, Helpers.CreateBiomeData(Data.BiomeData.Geyserite), Data.OutcropData.Geyserite);
            SiltstoneOutcrop = Helpers.CreateOutcrop("SiltstoneOutcrop",  "Siltstone outcrop",  "A siltstone outcrop.",  TechType.LimestoneChunk, Helpers.CreateBiomeData(Data.BiomeData.Siltstone), Data.OutcropData.Siltstone);
            SerpentiteOutcrop = Helpers.CreateOutcrop("SerpentiteOutcrop", "Serpentite outcrop", "A serpentite outcrop.", TechType.SandstoneChunk, Helpers.CreateBiomeData(Data.BiomeData.Serpentite), Data.OutcropData.Serpentite);

            Logger.LogInfo($"{pluginName} {versionString} has been loaded! (yay)");
        }
    }
}