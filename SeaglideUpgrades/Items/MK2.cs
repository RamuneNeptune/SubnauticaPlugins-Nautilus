
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using RamuneLib;
using UnityEngine;
using static CraftData;

namespace Ramune.SeaglideUpgrades.Items
{
    public static class MK2
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("SeaglideMK2", "Seaglide <color=#bde170>MK2</color>", "SPEED: +25%\nMay need to re-equip to apply speed", Utilities.GetSprite("MK2"), 2, 3);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.Seaglide)
            {
                ModifyPrefab = go =>
                {
                    var renderer = go.GetComponentInChildren<SkinnedMeshRenderer>(true);
                    go.GetComponentsInChildren<SkinnedMeshRenderer>(true).ForEach(x => x.material.mainTexture = Utilities.GetTexture("MK2_Tex"));
                    go.GetComponentsInChildren<SkinnedMeshRenderer>(true).ForEach(x => x.material.SetTexture("_Illum", Utilities.GetTexture("MK2_Illum")));
                }
            };
            prefab.SetGameObject(clone);
            prefab.SetUnlock(TechType.Seaglide);
            prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Tools);
            prefab.AddGadget(new EquipmentGadget(prefab, EquipmentType.Hand).WithQuickSlotType(QuickSlotType.Selectable));
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(MK1.Info.TechType, 1),
                new Ingredient(TechType.PlasteelIngot, 1),
                new Ingredient(TechType.ComputerChip, 1),
                new Ingredient(TechType.EnameledGlass, 2),
                new Ingredient(TechType.Diamond, 2)))
                .WithFabricatorType(CraftTree.Type.Workbench)
                .WithStepsToFabricatorTab("Seaglide")
                .WithCraftingTime(5.5f);

            prefab.Register();
        }
    }
}