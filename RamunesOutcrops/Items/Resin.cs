using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuddleLibs.Assets.Gadgets;

namespace Ramune.RamunesOutcrops.Items
{
    public static class Resin
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("Resin", "Resin", "Resin", Utilities.GetSprite(TechType.Silicone), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(prefab.Info, TechType.Silicone)
            {
                ModifyPrefab = go =>
                {

                }
            };

            prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);
            prefab.SetGameObject(clone);
            prefab.Register();
        }
    }
}