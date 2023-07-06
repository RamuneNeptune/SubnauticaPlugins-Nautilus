

namespace Ramune.PortableFabricator.Craftables
{
    public static class PortableFabricator
    {
        public static PrefabInfo Info;

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("PortableFabricator", "Portable fabricator", "Fabricator accessible from anywhere.", Utilities.GetSprite(TechType.Fabricator), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.Glass)
            {
                ModifyPrefab = go => Player.main.gameObject.EnsureComponent<Monos.PortableFabricatorHandler>()
            };

            prefab.SetGameObject(clone);
            prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);
            prefab.SetEquipment(EquipmentType.None).WithQuickSlotType(QuickSlotType.Selectable);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.PowerCell, 1),
                new Ingredient(TechType.ComputerChip, 1),
                new Ingredient(TechType.Silicone, 2),
                new Ingredient(TechType.PlasteelIngot, 1)))
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Personal", "Equipment");
            prefab.Register();
        }
    }
}