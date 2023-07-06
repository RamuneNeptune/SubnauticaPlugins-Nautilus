using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWE;

namespace Ramune.StackableJumpJets.Items
{
    public static class StackableJumpJetUpgrade
    {
        public static PrefabInfo Info;

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("PrawnSuitJumpJetUpgrade", "Prawn suit jump jet upgrade", "Powerful rear-mounted jets propel the Prawn suit into the air. Mutliple upgrades may be installed simultaneously.", Utilities.GetSprite(TechType.ExosuitJetUpgradeModule), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(prefab.Info, TechType.ExosuitJetUpgradeModule);

            prefab.SetGameObject(clone);
            prefab.SetEquipment(EquipmentType.ExosuitModule).WithQuickSlotType(QuickSlotType.SelectableChargeable);

            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.Quartz, 1),
                new Ingredient(TechType.Diamond, 1),
                new Ingredient(TechType.Kyanite, 1)))
               .WithFabricatorType(CraftTree.Type.SeamothUpgrades)
               .WithStepsToFabricatorTab("ExosuitUpgrades");

            prefab.SetVehicleUpgradeModule(EquipmentType.ExosuitModule, QuickSlotType.Passive)
                .WithOnModuleAdded((Vehicle vehicle, int slotId) => prefab.Register())            
                .WithOnModuleRemoved((Vehicle vehicle, int slotId) => prefab.Register());

            prefab.SetPdaGroupCategory(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades)
                .RequiredForUnlock = TechType.BaseUpgradeConsole;

            prefab.Register();
        }

        public static void RefreshUpgrades(Vehicle vehicle)
        {
            var exosuit = vehicle.GetComponentInChildren<Exosuit>();
            if(exosuit == null)
            {
                StackableJumpJets.logger.LogError("Component for 'Exosuit' not found on vehicle");
                return;
            }
        }
    }
}