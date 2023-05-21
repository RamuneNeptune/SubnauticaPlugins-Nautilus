
using static CraftData;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using RamuneLib;

namespace Ramune.StasisRifleUpgrades.Items
{
    public static class StasisRifleMK3
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("StasisRifleMK3", "Stasis rifle MK3", "A mega stasis rifle cool wow yeah", Utilities.GetSprite("MK3_"), 2, 2);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.StasisRifle)
            {
                ModifyPrefab = go =>
                {
                    var stasis = go.EnsureComponent<StasisRifle>();
                }
            };

            prefab.SetGameObject(clone);
            prefab.SetUnlock(TechType.StasisRifle);
            prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Tools);
            prefab.AddGadget(new EquipmentGadget(prefab, EquipmentType.Hand).WithQuickSlotType(QuickSlotType.Selectable));
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.StasisRifle, 1),
                new Ingredient(TechType.PlasteelIngot, 1),
                new Ingredient(TechType.AdvancedWiringKit, 1)))
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Resources", "BasicMaterials")
                .WithCraftingTime(0.5f);

            prefab.Register();
        }
    }
}