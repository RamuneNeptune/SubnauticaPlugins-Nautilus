using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramune.RadiantResources.Items.Resources
{
    public static class ElectrumAlloyIngot
    {
        public static PrefabInfo Info;
        public static Sprite Sprite = Utilities.GetSprite("ElectrumAlloyIngot.Sprite");

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("ElectrumAlloyIngot", "Electrum alloy ingot", "An electrum alloy ingot.", Sprite, 1, 1);

            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(prefab.Info, TechType.PlasteelIngot)
            {
                ModifyPrefab = go =>
                {

                }
            };

            prefab.SetGameObject(clone);
            prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);
            prefab.SetUnlock(TechType.PlasteelIngot);
            prefab.Register();
        }
    }
}