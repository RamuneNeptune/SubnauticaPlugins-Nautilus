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
using UnityEngine;

namespace Ramune.RamunesOutcrops.Items
{
    public static class RadiantCube
    {
        public static PrefabInfo Info;

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("RadiantCube", "<color=#C858DF>Radiant</color> Cube", "High capacity energy source.", Utilities.GetSprite("RadiantCubeSprite"), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.PrecursorIonCrystal)
            {
                ModifyPrefab = go =>
                {
                    var renderer = go.GetComponentInChildren<MeshRenderer>();
                    foreach(var m in renderer.materials)
                    {
                        m.mainTexture = Utilities.GetTexture("RadiantCubeTexture");
                        m.SetTexture("_Illum", Utilities.GetTexture("RadiantCubeTexture"));
                        m.SetColor("_GlowColor", new Color(0.67f, 0.1f, 0.85f, 0.4f));
                    }
                    go.EnsureComponent<Battery>()._capacity = 300;
                }
            };

            prefab.SetUnlock(TechType.PrecursorIonCrystal);
            prefab.SetEquipment(EquipmentType.BatteryCharger).WithQuickSlotType(QuickSlotType.Instant);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.PrecursorIonCrystal, 1),
                new Ingredient(RadiantCrystal.Info.TechType, 1)))
                .WithFabricatorType(RadiantFabricator.CraftTreeType)
                .WithCraftingTime(0.5f);
            prefab.SetGameObject(clone);
            prefab.Register();
        }   
    }
}