

namespace Ramune.OrganizedWorkbench
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class OrganizedWorkbench : BaseUnityPlugin
    {
        private const string myGUID = "com.ramune.OrganizedWorkbench";
        private const string pluginName = "Organized Workbench";
        private const string versionString = "1.0.2";

        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Console.WriteLine($"Loaded [{pluginName} {versionString}]");
            logger = Logger;
        }

        public static void Workbench()
        {
            Atlas.Sprite tool = SpriteManager.Get(TechType.Builder);
            Atlas.Sprite equip = SpriteManager.Get(TechType.Tank);
            Atlas.Sprite vehicle = SpriteManager.Get(TechType.Constructor);

            string[][] itemsToRemove = new string[][] {
                new string[] { "LithiumIonBattery" },
                new string[] { "HeatBlade" },
                new string[] { "PlasteelTank" },
                new string[] { "HighCapacityTank" },
                new string[] { "UltraGlideFins" },
                new string[] { "SwimChargeFins" },
                new string[] { "RepulsionCannon" },
                new string[] { "CyclopsHullModule2" },
                new string[] { "CyclopsHullModule3" },
                new string[] { "SeamothHullModule2" },
                new string[] { "SeamothHullModule3" },
                new string[] { "ExoHullModule2" }
            };

            foreach (string[] i in itemsToRemove) CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, i);

            CraftTreeHandler.AddTabNode(CraftTree.Type.Workbench, "Tools", "Tools", tool);
            CraftTreeHandler.AddTabNode(CraftTree.Type.Workbench, "Equipment", "Equipment", equip);
            CraftTreeHandler.AddTabNode(CraftTree.Type.Workbench, "Modules", "Modules", vehicle);

            string[] Tools = new string[] { "Tools" };
            string[] Equipment = new string[] { "Equipment" };
            string[] Modules = new string[] { "Modules" };

            Dictionary<TechType, string> workbench = new Dictionary<TechType, string>
            {
                { TechType.HeatBlade, "Tool" },
                { TechType.DiamondBlade, "Tool" },
                { TechType.RepulsionCannon, "Tool" },
                { TechType.PlasteelTank, "Equipment" },
                { TechType.HighCapacityTank, "Equipment" },
                { TechType.UltraGlideFins, "Equipment" },
                { TechType.SwimChargeFins, "Equipment" },
                { TechType.CyclopsHullModule2, "Vehicle" },
                { TechType.CyclopsHullModule3, "Vehicle" },
                { TechType.ExoHullModule2, "Vehicle" },
                { TechType.VehicleHullModule2, "Vehicle" },
                { TechType.VehicleHullModule3, "Vehicle" }
            };

            foreach (var techType in workbench)
            {
                switch (techType.Value)
                {
                    case "Tool":
                        CraftTreeHandler.AddCraftingNode(CraftTree.Type.Workbench, techType.Key, Tools);
                        break;
                    case "Equipment":
                        CraftTreeHandler.AddCraftingNode(CraftTree.Type.Workbench, techType.Key, Equipment);
                        break;
                    case "Vehicle":
                        CraftTreeHandler.AddCraftingNode(CraftTree.Type.Workbench, techType.Key, Modules);
                        break;
                }
            }
        }
    }
}