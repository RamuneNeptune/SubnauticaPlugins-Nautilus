
global using Nautilus.Assets.PrefabTemplates;
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
using System.Text.RegularExpressions;
using System.IO;
using Nautilus.Json.ExtensionMethods;

namespace Ramune.CustomCraftConverter
{
    public struct Recipe
    {
        internal string id;
        internal string amount;
        internal List<(string, string)> ingredients;

        public Recipe(string id_, string amount_, List<(string, string)> ingredients_)
        {
            id = id_;
            amount = amount_;
            ingredients = ingredients_;
        }

        public string GetRecipe()
        {
            var recipe = $"\nCraftDataHandler.SetRecipeData(TechType.{id}, Utilities.CreateRecipe({amount},";
            for(int i = 0; i < ingredients.Count; i++)
            {
                recipe += $"\n    new Ingredient(TechType.{ingredients[i].Item1}, {ingredients[i].Item2}),";
                recipe += $"\n    new Ingredient(TechType.{ingredients[i].Item1}, {ingredients[i].Item2})));";
            }
            recipe += "\n";
            return recipe;
        }
    }

    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class CustomCraftConverter : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.CustomCraftConverter";
        private const string pluginName = "Custom Craft Converter";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            logger = Logger;
            harmony.PatchAll();
            Main.FindPiracy();
            StartRegex();
            Console.WriteLine($"Loaded [{pluginName} {versionString}]");
        }

        public void StartRegex()
        {
            List<Recipe> recipes = new();
            var fileContents = File.ReadAllText("OriginalRecipes.txt");
            var regex = new Regex("ItemID:([^;\\n]+);\\s*AmountCrafted:(\\d+);\\s*Ingredients:\\s*((?:\\(\\s*ItemID:([^;\\n]+);\\s*Required:(\\d+);\\s*\\),?\\s*)+)");
            var regex_ = new Regex("\\(\\s*ItemID:([^;\\n]+);\\s*Required:(\\d+);\\s*");
            var all = "";

            //List<(string, string)> ingredients = new();
            //List<string> items = new();
            //List<string> amounts = new();

            foreach(Match match in regex.Matches(fileContents)) 
            {
                var item = match.Groups[1].Value;
                var amount = match.Groups[2].Value;
                var ingredients = match.Groups[3].Value;
                var actualIngredients = "";

                foreach(Match match_ in regex_.Matches(ingredients))
                {
                    actualIngredients += $"\n    new Ingredient(TechType.{match_})";
                }


                for(int i = 0; i < match.Groups[1].Captures.Count; i++)
                {
                    items.Add(match.Groups[1].Value[i].ToString());
                    amounts.Add(match.Groups[2].Value[i].ToString());

                    logger.LogFatal($"{match.Groups[1].Value[i]}");
                    logger.LogFatal($"{match.Groups[2].Value[i]}");

                    logger.LogFatal($"{match.Groups[1].Value[i]}");
                    logger.LogFatal($"{match.Groups[2].Value[i]}");
                }
            }

            foreach(Match match in regex.Matches(fileContents)) recipes.Add(new Recipe(match.Groups[1].Value, match.Groups[2].Value, ingredients));
            foreach(var r in recipes) all += r.GetRecipe();
            File.WriteAllText("OutputRecipes.txt", all);
        }
    }
}