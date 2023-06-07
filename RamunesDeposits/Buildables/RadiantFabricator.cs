


namespace Ramune.RamunesOutcrops.Buildables
{
    public static class RadiantFabricator
    {
        public static CraftTree.Type CraftTreeType;
        public static Fabricator Fabricator;

        public static void Patch()
        {
            var prefab = new CustomPrefab("RadiantFabricator", "Radiant fabricator", "A fabricator used to enhance technology with Radiant Crystals.", Utilities.GetSprite(TechType.Fabricator));
            var fabTree = prefab.CreateFabricator(out CraftTree.Type fabTreeType);
            var model = new FabricatorTemplate(prefab.Info, fabTreeType)
            {
                FabricatorModel = FabricatorTemplate.Model.Fabricator,
                ModifyPrefab = go =>
                {
                    var fabricator = go.GetComponent<Fabricator>();
                    //var leftBeam = fabricator.leftBeam.GetComponentsInChildren<ParticleSystem>(true); foreach (var p in leftBeam) p.startColor = new Color(0.67f, 0.1f, 0.85f, 0.4f);
                    //var rightBeam = fabricator.rightBeam.GetComponentsInChildren<ParticleSystem>(true); foreach (var p in rightBeam) p.startColor = new Color(0.67f, 0.1f, 0.85f, 0.4f);
                    //var sparksL = fabricator.sparksL.GetComponentsInChildren<ParticleSystem>(true); foreach (var p in sparksL) p.startColor = new Color(0.67f, 0.1f, 0.85f, 0.4f);
                    //var sparksR = fabricator.sparksR.GetComponentsInChildren<ParticleSystem>(true); foreach (var p in sparksR) p.startColor = new Color(0.67f, 0.1f, 0.85f, 0.4f);
                    //var sparksPS = fabricator.sparksPS.GetComponentsInChildren<ParticleSystem>(true); foreach (var p in sparksPS) p.startColor = new Color(0.67f, 0.1f, 0.85f, 0.4f);
                    fabricator.handOverText = "Use radiant fabricator";

                    var renderer = go.GetComponentInChildren<SkinnedMeshRenderer>(true);
                    renderer.material.mainTexture = Utilities.GetTexture("RadiantFabricatorTexture");
                    renderer.material.SetTexture("_SpecTex", Utilities.GetTexture("RadiantFabricatorTexture"));
                    renderer.material.SetTexture("_Illum", Utilities.GetTexture("RadiantFabricatorIllumTexture"));

                    go.GetComponent<Fabricator>().pickupOutOfRange = true;
                    Fabricator = go.GetComponent<Fabricator>();
                }
            };
            prefab.SetGameObject(model);
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 2), new Ingredient(TechType.Quartz, 2), new Ingredient(TechType.JeweledDiskPiece, 1)));
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            prefab.Register();

            CraftTreeHandler.AddTabNode(fabTreeType, "Tools", "Tools", Utilities.GetSprite("RadiantFabricatorToolsTabSprite"));
            CraftTreeHandler.AddTabNode(fabTreeType, "Equipment", "Equipment", Utilities.GetSprite("RadiantFabricatorEquipmentTabSprite"));
            CraftTreeHandler.AddTabNode(fabTreeType, "Electronics", "Electronics", Utilities.GetSprite("RadiantFabricatorElectronicsTabSprite"));
            CraftTreeType = fabTreeType;
        }
    }
}