

namespace Ramune.PrawnSuitReinforcements
{
    public static class Helpers
    {
        public static TechType MK1;
        public static TechType MK2;
        public static TechType MK3;
        public static TechType MK4;
        public static float maxHealth;

        public static void CreateUpgrade(string id, string name, string description, float healthToAdd)
        {
            var info = Utilities.CreatePrefabInfo(id, name, description, Utilities.GetSprite(TechType.HullReinforcementModule), 1, 1);
            var prefab = new CustomPrefab(info);
            var clone = new CloneTemplate(prefab.Info, TechType.HullReinforcementModule);

            prefab.SetGameObject(clone);
            prefab.SetEquipment(EquipmentType.ExosuitModule).WithQuickSlotType(QuickSlotType.Passive);

            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.Quartz, 1),
                new Ingredient(TechType.Diamond, 1),
                new Ingredient(TechType.Kyanite, 1)))
               .WithFabricatorType(CraftTree.Type.SeamothUpgrades)
               .WithStepsToFabricatorTab("ExosuitUpgrades");

            prefab.SetVehicleUpgradeModule(EquipmentType.ExosuitModule, QuickSlotType.Passive)
                .WithOnModuleAdded((Vehicle vehicle, int slotId) => vehicle.liveMixin.data.maxHealth += healthToAdd)
                .WithOnModuleRemoved((Vehicle vehicle, int slotId) => vehicle.liveMixin.data.maxHealth -= healthToAdd);

            prefab.SetPdaGroupCategory(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades)
                .RequiredForUnlock = TechType.BaseUpgradeConsole;

            prefab.Register();
        }
    }
}