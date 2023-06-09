
using static CraftData;
using Ramune.RamunesOutcrops.Craftables;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets;
using UnityEngine;
using RamuneLib;

namespace Ramune.RamunesOutcrops.Buildables
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
                    storage.height = 4;
                    storage.width = 4;
                    storage.Resize(4, 4);

                    MeshRenderer[] renderers = locker.GetComponentsInChildren<MeshRenderer>();
                    foreach(var r in renderers)
                    {
                        foreach(var m in r.materials)
                        {

                        }
                    }
                }
            };

            prefab.SetGameObject(clone);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule).SetBuildable(true);
            //prefab.SetUnlock(RadiantCrystal.Info.TechType);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.Quartz, 1),
                new Ingredient(TechType.Titanium, 2),
                new Ingredient(RadiantCrystal.Info.TechType, 1)));

            prefab.Register();
        }
    }
}