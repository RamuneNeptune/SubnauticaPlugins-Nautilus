

using System;
using System.Collections.Generic;
using Nautilus.Utility;
using System.Reflection;
using UnityEngine;
using System.IO;
using Nautilus.Assets;
using Nautilus.Crafting;
using static CraftData;

namespace RamuneLib
{
    public static class Utilities
    {
        /// <summary>
        /// Displays colored text on the screen temporarily
        /// </summary>
        /// <param name="color"><see cref="Colors"/></param>
        /// <param name="text">Text to display</param>
        public static void Log(string color, string text) => ErrorMessage.AddError(color + text + "</color>");


        public static PrefabInfo CreatePrefabInfo(string id, string name, string description, Atlas.Sprite sprite, int sizeX, int sizeY)
        {
            return PrefabInfo
                .WithTechType(id, name, description)
                .WithIcon(sprite)
                .WithSizeInInventory(new Vector2int(sizeX, sizeY));
        }


        /// <summary>
        /// Get a sprite from the game by TechType, or from the Assets folder by string
        /// </summary>
        /// <param name="FileOrTechType"></param>
        /// <returns>An <see cref="Atlas.Sprite"/></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Atlas.Sprite GetSprite(object FileOrTechType)
        {
            if (FileOrTechType is TechType techType) return SpriteManager.Get(techType);
            else if (FileOrTechType is string filename) return ImageUtils.LoadSpriteFromFile(IOUtilities.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", filename + ".png"));
            else throw new ArgumentException($"Incorrect type of '{FileOrTechType}' used in Sprite.Get()");
        }


        /// <summary>
        /// Gets a sprite from the Assets folder by string
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>A <see cref="Texture2D"/></returns>
        public static Texture2D GetTexture(string filename) => ImageUtils.LoadTextureFromFile(IOUtilities.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", filename + ".png"));


        /// <summary>
        /// Creates a <see cref="RecipeData"/> using a craft amount, and params for ingredients
        /// </summary>
        /// <param name="amountToCraft"><see cref="int"/> amount of items the recipe will craft</param>
        /// <param name="ingredients"><see cref="Ingredient"/>'s for the recipe</param>
        /// <returns>A <see cref="RecipeData"/></returns>
        public static RecipeData CreateRecipe(int craftAmount, params Ingredient[] ingredients)
        {
            RecipeData recipe = new RecipeData
            {
                craftAmount = craftAmount,
                Ingredients = new List<Ingredient>(ingredients)
            };
            return recipe;
        }
    }

    public static class Colors
    {
        public static string Red = "<color=#ff0000>";
        public static string Rorange = "<color=#ff4800>";
        public static string Orange = "<color=#ff9100>";
        public static string Amber = "<color=#ffaa00>";
        public static string Yellow = "<color=#ffd500>";
        public static string Lemon = "<color=#d9ff00>";
        public static string Lime = "<color=#91ff00>";
        public static string Green = "<color=#00ff00>";
        public static string Emerald = "<color=#00ff91>";
        public static string Aqua = "<color=#00ffd9>";
        public static string Cyan = "<color=#00d9ff>";
        public static string LightBlue = "<color=#0091ff>";
        public static string Blue = "<color=#0048ff>";
        public static string Purple = "<color=#7300ff>";
        public static string Pink = "<color=#ff00ff>";
        public static string White = "<color=ffffff>";
        public static string Grey = "<color=>";
        public static string Black = "<color=000000>";
    }
}