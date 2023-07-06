using System.Collections;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using static CraftData;
using UnityEngine;
using RamuneLib;
using Nautilus.Assets.Gadgets;
using UWE;


namespace Ramune.PrawnSuitTurboModule.Items
{
    public static class PrawnSuitTurboUpgrade
    {
        public static PrefabInfo Info;

        public static float forward;
        public static float backward;
        public static float sideward;

        public static float maxCharge = 10f;
        public static float cooldown = 5f;
        public static float energyCost = 5f;


        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("ExosuitTurboModule", "Prawn suit turbo module", "Allows the PRAWN to trigger a temporary speed boost at the cost of increased power usage. Incompatible with module stacking.", Utilities.GetSprite(TechType.ExosuitJetUpgradeModule), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(prefab.Info, TechType.ExosuitJetUpgradeModule);

            prefab.SetGameObject(clone);
            prefab.SetEquipment(EquipmentType.ExosuitModule).WithQuickSlotType(QuickSlotType.SelectableChargeable);

            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.Quartz, 1),
                new Ingredient(TechType.Diamond, 1),
                new Ingredient(TechType.Kyanite, 1)))
               .WithFabricatorType(CraftTree.Type.SeamothUpgrades)
               .WithStepsToFabricatorTab("ExosuitUpgrades")
               .WithCraftingTime(5f);

            prefab.SetVehicleUpgradeModule(EquipmentType.ExosuitModule, QuickSlotType.SelectableChargeable)
                .WithCooldown(cooldown)
                .WithMaxCharge(maxCharge)
                .WithEnergyCost(energyCost)
                .WithOnModuleUsed((Vehicle vehicle, int slotID, float charge, float chargeScalar) => CoroutineHost.StartCoroutine(ActivateTurbo(vehicle, charge)));

            prefab.SetPdaGroupCategory(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades)
                .RequiredForUnlock = TechType.BaseUpgradeConsole;

            prefab.Register();
        }

        public static IEnumerator ActivateTurbo(Vehicle vehicle, float charge)
        {
            forward = vehicle.forwardForce;
            backward = vehicle.backwardForce;
            sideward = vehicle.sidewardForce;

            vehicle.forwardForce = forward * 5f;
            vehicle.backwardForce = backward * 5f;
            vehicle.sidewardForce = sideward * 5f;

            yield return new WaitForSeconds(1.2f);

            vehicle.forwardForce = forward / 1.4f;
            vehicle.backwardForce = backward / 1.4f;
            vehicle.sidewardForce = sideward / 1.4f;

            yield return new WaitForSeconds(0.1f);

            vehicle.forwardForce = forward;
            vehicle.backwardForce = backward;
            vehicle.sidewardForce = sideward;
        }
    }
}