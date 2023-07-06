

namespace Ramune.SeamothTurbo.Items
{
    public static class TurboModule
    {
        public static PrefabInfo Info;

        public static float forward;
        public static float backward;
        public static float sideward;

        public static float maxCharge = 10f;
        public static float cooldown = 5f;


        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("SeamothTurboModule", "Turbo module", "A booster for your SEAMOTH.", Utilities.GetSprite(TechType.SeamothElectricalDefense), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(prefab.Info, TechType.SeamothElectricalDefense);

            prefab.SetGameObject(clone);
            prefab.SetEquipment(EquipmentType.SeamothModule).WithQuickSlotType(QuickSlotType.SelectableChargeable);

            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.Quartz, 1),
                new Ingredient(TechType.Diamond, 1),
                new Ingredient(TechType.Kyanite, 1))) 
               .WithFabricatorType(CraftTree.Type.SeamothUpgrades);

            prefab.SetVehicleUpgradeModule(EquipmentType.SeamothModule, QuickSlotType.SelectableChargeable)
                .WithEnergyCost(10f)
                .WithCooldown(cooldown)
                .WithMaxCharge(maxCharge)
                .WithOnModuleUsed((Vehicle vehicle, int slotID, float charge, float chargeScalar) => CoroutineHost.StartCoroutine(ActivateTurbo(vehicle, charge)));
            
            prefab.Register();
        }

        public static IEnumerator ActivateTurbo(Vehicle vehicle, float charge)
        {
            SeamothTurbo.logger.LogInfo("Module activated");
            ErrorMessage.AddMessage("Module activated");
            Subtitles.Add("Module activated");

            var seamoth = vehicle.gameObject.GetComponentInChildren<SeaMoth>();
            var engine = vehicle.gameObject.GetComponentInChildren<EngineRpmSFXManager>();

            Utilities.Log(Colors.Yellow, "Grabbing components..");

            forward = vehicle.forwardForce;
            backward = vehicle.backwardForce;
            sideward = vehicle.sidewardForce;

            vehicle.gameObject.GetComponentInChildren<Rigidbody>().AddForce(vehicle.transform.forward, ForceMode.VelocityChange);

            vehicle.forwardForce = forward * 5;
            vehicle.backwardForce = backward * 5;
            vehicle.sidewardForce = sideward * 5;

            Utilities.Log(Colors.Green, "Speeding the mf up..");

            engine.engineRpmSFX.GetEventInstance().setPitch(1.25f);
            engine.engineRpmSFX.GetEventInstance().setVolume(1.5f);

            yield return new WaitForSeconds(1.2f);

            vehicle.forwardForce = forward / 2;
            vehicle.backwardForce = backward / 2;
            vehicle.sidewardForce = sideward / 2;

            yield return new WaitForSeconds(0.1f);

            vehicle.forwardForce = forward;
            vehicle.backwardForce = backward;
            vehicle.sidewardForce = sideward;

            engine.engineRpmSFX.GetEventInstance().setPitch(1f);
            engine.engineRpmSFX.GetEventInstance().setVolume(1f);
        }
    }
}