
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using RamuneLib;
using UnityEngine;
using static CraftData;

namespace Ramune.SeaglideUpgrades.Items
{
    public static class MK3
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("SeaglideMK3", "Seaglide <color=#f81117>MK3</color>", "SPEED: +40%\nMay need to re-equip to apply speed", Utilities.GetSprite("MK3"), 2, 3);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.Seaglide)
            {
                ModifyPrefab = go =>
                {
                    var renderer = go.GetComponentInChildren<SkinnedMeshRenderer>(true);
                    go.GetComponentsInChildren<SkinnedMeshRenderer>(true).ForEach(x => x.material.mainTexture = Utilities.GetTexture("MK3_Tex"));
                    go.GetComponentsInChildren<SkinnedMeshRenderer>(true).ForEach(x => x.material.SetTexture("_Illum", Utilities.GetTexture("MK3_Illum")));
                }
            };
            prefab.SetGameObject(clone);
            prefab.SetUnlock(TechType.Seaglide);
            prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Tools);
            prefab.AddGadget(new EquipmentGadget(prefab, EquipmentType.Hand).WithQuickSlotType(QuickSlotType.Selectable));
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(MK2.Info.TechType, 1),
                new Ingredient(TechType.PlasteelIngot, 2),
                new Ingredient(TechType.AdvancedWiringKit, 1),
                new Ingredient(TechType.UraniniteCrystal, 2),
                new Ingredient(TechType.Aerogel, 2)))
                .WithFabricatorType(CraftTree.Type.Workbench)
                .WithStepsToFabricatorTab("Seaglide")
                .WithCraftingTime(5.5f);

            prefab.Register();
        }
    }
}