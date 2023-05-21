
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using System.Collections.Generic;
using Nautilus.Handlers;
using Nautilus.Crafting;
using static CraftData;
using System;

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

        public static Dictionary<TechType, Tuple<string, string>> resources = new Dictionary<TechType, Tuple<string, string>>()
        {
            { TechType.Gold, Tuple.Create("Gold", "Au. Compressed gold") },
            { TechType.Silver, Tuple.Create("Silver", "Ag. Compressed silver") },
            { TechType.Quartz, Tuple.Create("Quartz", "SiO2. Compressed quartz") },
            { TechType.Titanium, Tuple.Create("Titanium", "Ti. Compressed titanium") },
            { TechType.Copper, Tuple.Create("Copper", "Cu. Compressed copper") },
            { TechType.UraniniteCrystal, Tuple.Create("Uraninite Crystal", "UO2. Compressed uraninite crystal") },
            { TechType.AluminumOxide, Tuple.Create("Ruby", "Al2O3. Compressed ruby") },
            { TechType.Diamond, Tuple.Create("Diamond", "C. Compressed diamond") },
        };

        public void Items()
        {
            CraftTreeHandler.AddTabNode(CraftTree.Type.Fabricator, "Packed", "Packed resources", Utilities.GetSprite(TechType.Peeper), "Resources");
            CraftTreeHandler.AddTabNode(CraftTree.Type.Fabricator, "Unpacked", "Unpacked resources", Utilities.GetSprite(TechType.Bladderfish), "Resources");
            foreach(var item in resources) AddPacked(item.Value.Item1, item.Value.Item2, item.Key);
        }

        public void AddPacked(string b, string e, TechType a)
        {
            //
        }

        /*
        public void AddPacked(string itemName, string itemDescription, TechType itemForCraft)
        {
            var item = EnumHandler.AddEntry<TechType>(itemName + "Ingot")
                .WithPdaInfo(itemName, itemDescription, unlockAtStart: false)
                .WithIcon(Utilities.GetSprite(itemCost));

            CraftDataHandler.SetRecipeData(item, Recipe(itemCost));
            AddUnpacked(itemName, item, itemCost);
        }

        public void AddUnpacked(string itemName, TechType itemCost, TechType linkedItems)
        {
            var item = EnumHandler.AddEntry<TechType>("Unpacked" + itemName)
                .WithPdaInfo(name, "A bunch of compressed " + name, unlockAtStart: false)
                .WithIcon(Utilities.GetSprite(linkedItems));
            CraftDataHandler.SetRecipeData(item, Recipe(itemCost, linkedItems));
            CraftTreeHandler.AddCraftingNode(CraftTree.Type.Fabricator, item, "Resources", "Unpacked");
            CraftTreeHandler.AddCraftingNode(CraftTree.Type.Fabricator, itemCost, "Resources", "Packed");
        }
        */

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