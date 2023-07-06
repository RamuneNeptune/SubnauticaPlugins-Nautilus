

namespace Ramune.RadiantResources
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class RadiantResources : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.RadiantResources";
        private const string pluginName = "Radiant Resources";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            logger = Logger;
            harmony.PatchAll();
            Main.FindPiracy();
            LoadItems();
        }

        public void LoadItems()
        {
            Items.Fabricators.ElectrumWorkbench.Patch();

            Logger.LogInfo($"{pluginName} {versionString} is loaded, brought to you by RamuneNeptune");
        }
    }
}