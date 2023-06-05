
using static CraftData;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets.Gadgets;
using Nautilus.Handlers;
using Nautilus.Crafting;
using Nautilus.Assets;
using UnityEngine;
using RamuneLib;

namespace Ramune.RamunesOutcrops.Buildables
{
    public static class RadiantFabricator
    {
        public static CraftTree.Type CraftTreeType;

        public static void Patch()
        {
            var prefab = new CustomPrefab("RadiantFabricator", "Radiant fabricator", "A fabricator used to enhance technology with Radiant Crystals.", Utilities.GetSprite(TechType.Fabricator));
            var fabTree = prefab.CreateFabricator(out CraftTree.Type fabTreeType);
            var model = new FabricatorTemplate(prefab.Info, fabTreeType)
            {
                FabricatorModel = FabricatorTemplate.Model.Fabricator,
                ModifyPrefab = go =>
                {
                    var renderer = go.GetComponentInChildren<SkinnedMeshRenderer>(true);
                    renderer.material.mainTexture = Utilities.GetTexture("RadiantFabricatorTexture");
                    renderer.material.SetTexture("_SpecTex", Utilities.GetTexture("RadiantFabricatorTexture"));
                    renderer.material.SetTexture("_Illum", Utilities.GetTexture("RadiantFabricatorIllumTexture"));
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