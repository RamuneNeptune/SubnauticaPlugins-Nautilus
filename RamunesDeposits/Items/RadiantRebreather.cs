using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using Ramune.RamunesOutcrops.Fabricators;
using static CraftData;
using RamuneLib;
using Nautilus.Assets.Gadgets;

namespace Ramune.RamunesOutcrops.Items
{
    public static class RadiantRebreather
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("RadiantRebreather", "<color=#C858DF>Radiant</color> Rebreather", "Conserves oxygen when diving deeper. Absorbs and recycles CO2 into breathable air.\n\nEFFICIENCY: +50%", Utilities.GetSprite("RadiantRebreatherSprite"), 2, 2);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.Rebreather);

            prefab.SetGameObject(clone);
            prefab.SetUnlock(TechType.Rebreather);
            prefab.SetEquipment(EquipmentType.Head).WithQuickSlotType(QuickSlotType.None);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.Rebreather, 1),
                new Ingredient(RadiantCrystal.Info.TechType, 2)))
                .WithFabricatorType(RadiantFabricator.CraftTreeType)
                .WithStepsToFabricatorTab("Equipment")
                .WithCraftingTime(0.5f);
            prefab.Register();
        }
    }
}