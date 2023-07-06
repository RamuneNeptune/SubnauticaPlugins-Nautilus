
global using static Ramune.RamunesOutcrops.RamunesOutcrops;
global using Ramune.RamunesOutcrops.Buildables;
global using Ramune.RamunesOutcrops.Craftables;
global using Nautilus.Assets.PrefabTemplates;
global using Ramune.RamunesOutcrops.Outcrops;
global using UnityEngine.AddressableAssets;
global using System.Collections.Generic;
global using Nautilus.Assets.Gadgets;
global using Nautilus.Extensions;
global using System.Collections;
global using Nautilus.Handlers;
global using Nautilus.Crafting;
global using Nautilus.Utility;
global using static CraftData;
global using BepInEx.Logging;
global using Nautilus.Assets;
global using UnityEngine;
global using System.Linq;
global using HarmonyLib;
global using RamuneLib;
global using BepInEx;
global using System;

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
            logger = Logger;
            harmony.PatchAll();
            Main.FindPiracy();
            LoadItems();
        }

        public void LoadItems()
        {
            var prefab = new CustomPrefab("Bleh", "Bleh", "blehh", Utilities.GetSprite(TechType.BasaltChunk));
            var clone = new CloneTemplate(prefab.Info, TechType.AcidMushroom);
            prefab.SetGameObject(clone);
            prefab.SetRecipe(Utilities.CreateRecipe(1, new Ingredient(TechType.Quartz, 2)));
            prefab.Register();
            /*
            Craftables.RadiantCrystal.Patch();
            Buildables.RadiantFabricator.Patch();
            Buildables.RadiantLocker.Patch();
            Buildables.RadiantWallLocker.Patch();
            Craftables.RadiantThermoblade.Patch();
            Craftables.RadiantFins.Patch();
            Craftables.RadiantCube.Patch();
            Craftables.RadiantTank.Patch();
            Craftables.RadiantSeaglide.Patch();
            Craftables.RadiantRebreather.Patch();
            */
            LoadOutcrops();
        }

        public void LoadOutcrops()
        {
            LodestoneOutcrop = Helpers.CreateOutcrop("LodestoneOutcrop", "Lodestone outcrop", "A lodestone outcrop.", TechType.BasaltChunk, Helpers.CreateBiomeData(BiomeData.Lodestone), BreakableData.Lodestone);
            GeyseriteOutcrop = Helpers.CreateOutcrop("GeyseriteOutcrop", "Geyserite outcrop", "A geyserite outcrop.", TechType.ShaleChunk, Helpers.CreateBiomeData(BiomeData.Geyserite), BreakableData.Geyserite);
            SiltstoneOutcrop = Helpers.CreateOutcrop("SiltstoneOutcrop", "Siltstone outcrop", "A siltstone outcrop.", TechType.LimestoneChunk, Helpers.CreateBiomeData(BiomeData.Siltstone), BreakableData.Siltstone);
            SerpentiteOutcrop = Helpers.CreateOutcrop("SerpentiteOutcrop", "Serpentite outcrop", "A serpentite outcrop.", TechType.SandstoneChunk, Helpers.CreateBiomeData(BiomeData.Serpentite), BreakableData.Serpentite);
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
        }
    }
}