

namespace Ramune.SeamothTurbo.Items
{
    public static class TurboModule
    {
        public static PrefabInfo Info;

        public static float forward;
        public static float backward;
        public static float sideward;

        public static float maxCharge = 50f;
        public static float cooldown = 10f;


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
                .WithCooldown(cooldown)
                .WithMaxCharge(maxCharge)
                .WithOnModuleAdded((Vehicle vehicle, int slotId) => OnAdded())
                .WithOnModuleRemoved((Vehicle vehicle, int slotId) => OnRemoved())
                .WithOnModuleUsed((Vehicle vehicle, int slotID, float charge, float chargeScalar) => CoroutineHost.StartCoroutine(ActivateTurbo(vehicle, charge)));
            
            prefab.Register();
        }


        public static void OnAdded()
        {
            SeamothTurbo.logger.LogInfo("Module installed");
            ErrorMessage.AddMessage("Module installed");
            Subtitles.Add("Module installed");
        }

        public static void OnRemoved()
        {
            SeamothTurbo.logger.LogInfo("Module removed");
            ErrorMessage.AddMessage("Module removed");
            Subtitles.Add("Module removed");
        }

        public static IEnumerator ActivateTurbo(Vehicle vehicle, float charge)
        {
            SeamothTurbo.logger.LogInfo("Module activated");
            ErrorMessage.AddMessage("Module activated");
            Subtitles.Add("Module activated");

            if(charge != maxCharge) yield break;

            var seamoth = vehicle.gameObject.GetComponent<SeaMoth>();
            var engine = vehicle.gameObject.GetComponent<EngineRpmSFXManager>();

            forward = vehicle.forwardForce;
            backward = vehicle.backwardForce;
            sideward = vehicle.sidewardForce;

            vehicle.forwardForce = forward * 5;
            vehicle.backwardForce = backward * 5;
            vehicle.sidewardForce = sideward * 5;

            engine.engineRpmSFX.GetEventInstance().setPitch(1.15f);
            engine.engineRpmSFX.GetEventInstance().setVolume(1.25f);

            yield return new WaitForSeconds(1.85f);

            vehicle.forwardForce = forward;
            vehicle.backwardForce = backward;
            vehicle.sidewardForce = sideward;

            engine.engineRpmSFX.GetEventInstance().setPitch(1f);
            engine.engineRpmSFX.GetEventInstance().setVolume(1f);
        }
    }
}