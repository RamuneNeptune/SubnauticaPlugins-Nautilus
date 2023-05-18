
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using System.Collections.Generic;
using Nautilus.Handlers;
using Nautilus.Crafting;
using static CraftData;

namespace Ramune.MoreIngots
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class MoreIngots : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.MoreIngots";
        private const string pluginName = "More Ingots";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Items();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }

        public static List<TechType> packedTechTypes = new List<TechType>()
        {
            TechType.Gold,
            TechType.Silver,
            TechType.Quartz,
            TechType.Titanium,
            TechType.Copper,
            TechType.UraniniteCrystal,
            TechType.AluminumOxide,
            TechType.Diamond,
        };

        public void Items()
        {
            CraftTreeHandler.AddTabNode(CraftTree.Type.Fabricator, "Packed", "Packed resources", Utilities.GetSprite(TechType.Peeper), "Resources");
            CraftTreeHandler.AddTabNode(CraftTree.Type.Fabricator, "Unpacked", "Unpacked resources", Utilities.GetSprite(TechType.Bladderfish), "Resources");
            foreach(var item in packedTechTypes) AddPacked(item.ToString(), item);
        }

        public void AddPacked(string itemName, TechType itemCost)
        {
            var name = "Packed " + itemName.ToString();
            var item = EnumHandler.AddEntry<TechType>("Packed" + itemName)
                .WithPdaInfo(name, "A bunch of compressed " + name, unlockAtStart: false)
                .WithIcon(Utilities.GetSprite(itemCost));
            CraftDataHandler.SetRecipeData(item, Recipe(itemCost));
            AddUnpacked(itemName, item, itemCost);
        }

        public void AddUnpacked(string itemName, TechType itemCost, TechType linkedItems)
        {
            var name = "Unpacked " + itemName.ToString();
            var item = EnumHandler.AddEntry<TechType>("Unpacked" + itemName)
                .WithPdaInfo(name, "A bunch of compressed " + name, unlockAtStart: false)
                .WithIcon(Utilities.GetSprite(linkedItems));
            CraftDataHandler.SetRecipeData(item, Recipe(itemCost, linkedItems));
            CraftTreeHandler.AddCraftingNode(CraftTree.Type.Fabricator, item, "Resources", "Unpacked");
            CraftTreeHandler.AddCraftingNode(CraftTree.Type.Fabricator, itemCost, "Resources", "Packed");
        }

        public RecipeData Recipe(TechType ingredient, TechType linked = TechType.None)
        {
            if(linked == TechType.None)
            {
                return new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>()
                    {
                    new Ingredient(ingredient, 10)
                    }
                };
            }
            return new RecipeData()
            {
                craftAmount = 0,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(ingredient, 1)
                },
                LinkedItems = new List<TechType>()
                { 
                    linked, linked, linked, linked, linked, linked, linked, linked, linked, linked,
                }
            };
        }
    }
}