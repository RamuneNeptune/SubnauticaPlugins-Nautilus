using System;
using System.Collections.Generic;
using System.Text;
using Nautilus.Crafting;
using static CraftData;

namespace RamuneLib.Utils
{
    public static class Recipe
    {
        public static RecipeData Create(int craftAmount, params Ingredient[] ingredients)
        {
            RecipeData recipe = new RecipeData();
            recipe.craftAmount = craftAmount;
            recipe.Ingredients = new List<Ingredient>(ingredients);
            return recipe;
        }
    }
}