using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Ramune.RamunesOutcrops.Fabricators;
using Ramune.RamunesOutcrops.Items;
using RamuneLib;
using UnityEngine;
using static CraftData;

namespace Ramune.RamunesOutcrops.Constructables
{
    public static class RadiantLocker
    {
        public static PrefabInfo Info;

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("RadiantLocker", "Radiant locker", "A radiant locker.", Utilities.GetSprite(TechType.Locker), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.Locker)
            {
                ModifyPrefab = locker =>
                {
                    StorageContainer storage = locker.GetComponent<StorageContainer>();
                    storage.hoverText = "Open radiant locker";
                    storage.storageLabel = "Radiant locker";
                    storage.height = 5;
                    storage.width = 5;
                    storage.Resize(5, 5);

                    MeshRenderer[] renderers = locker.GetComponentsInChildren<MeshRenderer>();
                    foreach(var r in renderers)
                    {
                        r.material.color = new Color(0.67f, 0.1f, 0.85f, 0.4f);
                    }
                }
            };

            prefab.SetGameObject(clone);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule).SetBuildable(true);
            prefab.SetUnlock(RadiantCrystal.Info.TechType);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.Quartz, 1),
                new Ingredient(TechType.Titanium, 2),
                new Ingredient(RadiantCrystal.Info.TechType, 1)));

            prefab.Register();
        }
    }
}