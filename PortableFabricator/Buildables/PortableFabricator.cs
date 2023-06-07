


namespace Ramune.PortableFabricator.Buildables
{
    public static class PortableFabricator
    {
        public static CraftTree.Type CraftTreeType;
        public static Fabricator Fabricator;
        public static PrefabInfo Info;

        public static void Patch()
        {
            var prefab = new CustomPrefab("PortableFabricator_", "Portable fabricator", "A portable fabricator.", Utilities.GetSprite(TechType.Fabricator));
            var fabTree = prefab.CreateFabricator(out CraftTree.Type fabTreeType);
            var model = new FabricatorTemplate(prefab.Info, fabTreeType)
            {
                FabricatorModel = FabricatorTemplate.Model.Fabricator,
                ModifyPrefab = go => Fabricator = go.GetComponent<Fabricator>()
            };
            prefab.SetGameObject(model);
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 2), new Ingredient(TechType.Quartz, 2), new Ingredient(TechType.JeweledDiskPiece, 1)));
            prefab.Register();

            CraftTreeHandler.AddTabNode(fabTreeType, "Sustenance", "Sustenance", Utilities.GetSprite(TechType.Diamond));
            CraftTreeHandler.AddTabNode(fabTreeType, "Water", "Water", Utilities.GetSprite(TechType.Titanium), "Sustenance");

            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.FilteredWater,    "Sustenance", "Water");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.DisinfectedWater, "Sustenance", "Water");

            /*
            CraftTreeHandler.AddTabNode(fabTreeType, "CuredFood",    "Cured food", Utilities.GetSprite(TechType.Titanium), "Sustenance");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredHoleFish, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredPeeper, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredBladderfish, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredGarryFish, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredHoverfish, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredReginald, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredSpadefish, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredBoomerang, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredLavaBoomerang, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredEyeye, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredLavaEyeye, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredOculus, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredHoopfish, "Sustenance", "CuredFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CuredSpinefish, "Sustenance", "CuredFood");

            CraftTreeHandler.AddTabNode(fabTreeType, "CookedFood", "Cooked food", Utilities.GetSprite(TechType.Titanium), "Sustenance");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedHoleFish, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedPeeper, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedBladderfish, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedGarryFish, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedHoverfish, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedReginald, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedSpadefish, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedBoomerang, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedLavaBoomerang, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedEyeye, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedLavaEyeye, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedOculus, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedHoopfish, "Sustenance", "CookedFood");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.CookedSpinefish, "Sustenance", "CookedFood");
            */

            CraftTreeHandler.AddTabNode(fabTreeType, "Deployables", "Deployables", Utilities.GetSprite(TechType.Diamond));
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.Beacon, "Deployables");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.SmallStorage, "Deployables");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.Gravsphere, "Deployables");
            CraftTreeHandler.AddCraftingNode(fabTreeType, TechType.Flare, "Deployables");

            CraftTreeType = fabTreeType;
            Info = prefab.Info;
        }
    }
}