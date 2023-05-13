
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using RamuneLib;
using UnityEngine;
using static CraftData;

namespace Ramune.SeaglideUpgrades.Items
{
    public static class MK1
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("SeaglideMK1", "Seaglide <color=#03f0f1>MK1</color>", "SPEED: +15%\nMay need to re-equip to apply speed", Utilities.GetSprite("MK1"), 2, 3);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.Seaglide)
            {
                ModifyPrefab = go =>
                {
                    var renderer = go.GetComponentInChildren<SkinnedMeshRenderer>(true);
                    go.GetComponentsInChildren<SkinnedMeshRenderer>(true).ForEach(x => x.material.mainTexture = SeaglideUpgrades.MK1_Tex);
                    go.GetComponentsInChildren<SkinnedMeshRenderer>(true).ForEach(x => x.material.SetTexture("_Illum", SeaglideUpgrades.MK1_Illum));
                }
            };
            prefab.SetGameObject(clone);
            prefab.SetUnlock(TechType.Seaglide);
            prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Tools);
            prefab.AddGadget(new EquipmentGadget(prefab, EquipmentType.Hand).WithQuickSlotType(QuickSlotType.Selectable));
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.Seaglide, 1),
                new Ingredient(TechType.TitaniumIngot, 1),
                new Ingredient(TechType.WiringKit, 1),
                new Ingredient(TechType.Silicone, 2),
                new Ingredient(TechType.CrashPowder, 2)))                
                .WithFabricatorType(CraftTree.Type.Workbench)
                .WithStepsToFabricatorTab("Seaglide")
                .WithCraftingTime(5.5f);

            prefab.Register();
        }
    }
}