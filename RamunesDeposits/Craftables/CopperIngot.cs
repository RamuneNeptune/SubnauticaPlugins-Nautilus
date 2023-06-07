


namespace Ramune.RamunesOutcrops.Craftables
{
    public static class CopperIngot
    {
        public static PrefabInfo Info;

        public static void Patch()
        {
            CustomPrefab prefab = new CustomPrefab("CopperIngotItem", "Copper Ingot", "Better storage of copper.");
            CloneTemplate clone = new CloneTemplate(prefab.Info, TechType.TitaniumIngot)
            {
                ModifyPrefab = ingot =>
                {
                    // Can get the MeshRenderer and change its color or something like that here
                }
            };

            Info = prefab.Info;

            prefab.SetPdaGroupCategory(TechGroup.Miscellaneous, TechCategory.Misc);
            prefab.SetRecipe(new RecipeData() {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(TechType.Copper, 5),
                }}
            )
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Resources", "Electronics")
            .WithCraftingTime(1f);

            prefab.Register();
        }
    }
}