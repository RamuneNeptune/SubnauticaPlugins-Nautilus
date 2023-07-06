

namespace Ramune.RadiantResources.Items.Fabricators
{
    public static class ElectrumWorkbench
    {
        public static PrefabInfo Info;
        public static Texture2D MainTex = Utilities.GetTexture("ElectrumWorkbench.Texture.Main");
        public static Texture2D SpecTex = Utilities.GetTexture("ElectrumWorkbench.Texture.Specular");
        public static Texture2D IllumTex = Utilities.GetTexture("ElectrumWorkbench.Texture.Illum");
        public static Sprite Main = Utilities.GetSprite("ElectrumWorkbench.Sprite.Main");
        public static Sprite Resources = Utilities.GetSprite("ElectrumWorkbench.Sprite.Resources");
        public static Sprite Alloys = Utilities.GetSprite("ElectrumWorkbench.Sprite.Alloys");
        public static Sprite Electronics = Utilities.GetSprite("ElectrumWorkbench.Sprite.Electronics");
        public static CraftTree.Type CraftTree;

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("ElectrumWorkbench", "Electrum workbench", "An electrum workbench.", Main, 1, 1);

            var prefab = new CustomPrefab(Info);
            var craftTree = prefab.CreateFabricator(out CraftTree.Type craftTreeType);
            var model = new FabricatorTemplate(prefab.Info, craftTreeType)
            {
                FabricatorModel = FabricatorTemplate.Model.Workbench,
                ModifyPrefab = go =>
                {
                    var workbench = go.GetComponent<Workbench>();
                    workbench.handOverText = "Use radiant workbench";

                    var renderer = go.GetComponentInChildren<SkinnedMeshRenderer>(true);
                    renderer.material.SetTexture(ShaderPropertyID._MainTex, MainTex);
                    renderer.material.SetTexture(ShaderPropertyID._SpecTex, SpecTex);
                    renderer.material.SetTexture(ShaderPropertyID._Illum, IllumTex);
                }
            };

            prefab.SetGameObject(model);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.Gold, 1),
                new Ingredient(TechType.Diamond, 1),
                new Ingredient(TechType.PlasteelIngot, 2),
                new Ingredient(TechType.PrecursorIonCrystal, 2)));
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            prefab.Register();

            List<(string, string, Sprite)> Tabs = new()
            {
                new("Resources", null, Resources),
                new("Alloys", "Resources", Alloys),
                new("Electronics", "Resources", Electronics),
            };

            foreach(var t in Tabs)
            {
                if(t.Item2.IsNullOrWhiteSpace()) CraftTreeHandler.AddTabNode(craftTreeType, t.Item1,  t.Item1,  t.Item3);
                else CraftTreeHandler.AddTabNode(craftTreeType, t.Item1, t.Item1,  t.Item3,   t.Item2);
            }

            CraftTreeHandler.AddCraftingNode(craftTreeType, TechType.PlasteelIngot, "Resources", "Alloys");
            CraftTreeHandler.AddCraftingNode(craftTreeType, TechType.AdvancedWiringKit, "Resources", "Electronics");

            CraftTree = craftTreeType;
        }
    }
}