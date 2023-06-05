


namespace Ramune.RamunesOutcrops.Craftables
{
    public static class RadiantTank
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("RadiantTank", "Ultra High Capacity <color=#C858DF>Radiant</color> Tank", "Additional air capacity.", Utilities.GetSprite("RadiantTankSprite"), 3, 3);
            var prefab = new CustomPrefab(Info);

            prefab.SetUnlock(TechType.HighCapacityTank);
            prefab.SetEquipment(EquipmentType.Tank).WithQuickSlotType(QuickSlotType.Instant);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.HighCapacityTank, 1),
                new Ingredient(RadiantCrystal.Info.TechType, 2)))
                .WithFabricatorType(RadiantFabricator.CraftTreeType)
                .WithStepsToFabricatorTab("Equipment")
                .WithCraftingTime(0.5f);

            var clone = new CloneTemplate(Info, TechType.HighCapacityTank)
            {
                ModifyPrefab = go =>
                {
                    var energy = PrefabUtils.AddEnergyMixin(go, "RadiantTankEnergySlot", TechType.Battery, new List<TechType>() { TechType.Battery });
                    energy.allowBatteryReplacement = true;

                    Player.main.gameObject.EnsureComponent<RadiantTankMono>().mixin = energy;

                    go.EnsureComponent<Oxygen>().oxygenCapacity = 300;
                }
            };
            prefab.SetGameObject(clone);
            prefab.Register();
        }
    }

    public class RadiantTankMono : MonoBehaviour
    {
        public EnergyMixin mixin;

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.R) )
            {
                mixin.InitiateReload();
            }
        }
    }
}