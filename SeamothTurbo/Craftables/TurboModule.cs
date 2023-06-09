using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramune.SeamothTurbo.Craftables
{
    public static class TurboModule
    {
        public static PrefabInfo Info;

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("SeamothTurboModule", "Turbo module", "Allows for short engine overcharge bursts.", Utilities.GetSprite(TechType.SeamothReinforcementModule) 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.SeamothReinforcementModule)
            {
                ModifyPrefab = go =>
                {

                }
            };

            prefab.SetGameObject(clone);
            prefab.SetPdaGroupCategory(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades);
            prefab.SetEquipment(EquipmentType.SeamothModule).WithQuickSlotType(QuickSlotType.Chargeable);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.Quartz, 1),
                new Ingredient(TechType.Copper, 1)))
                .WithFabricatorType(CraftTree.Type.SeamothUpgrades)
                .WithStepsToFabricatorTab("");

            prefab.Register();
        }
    }
}