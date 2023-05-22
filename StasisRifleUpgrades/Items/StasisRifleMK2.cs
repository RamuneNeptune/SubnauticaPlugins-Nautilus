
using static CraftData;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using RamuneLib;
using UnityEngine;

namespace Ramune.StasisRifleUpgrades.Items
{
    public static class StasisRifleMK2
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("StasisRifleMK2", "Stasis rifle MK2", "A mega stasis rifle cool wow yeah", Utilities.GetSprite("MK2"), 2, 2);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.StasisRifle)
            {
                ModifyPrefab = go =>
                {
                    var renderers = go.GetComponentsInChildren<SkinnedMeshRenderer>(true);
                    foreach(var r in renderers)
                    {
                        foreach(var m in r.materials)
                        {
                            if(m.name.StartsWith("stasis")) m.mainTexture = Utilities.GetTexture("Stasis");

                            if(m.name.StartsWith("UI"))
                            {
                                m.SetTexture("_MainTex", Utilities.GetTexture("MK2_Off"));
                                m.SetTexture("_BarTex", Utilities.GetTexture("MK2_On"));
                            }

                            if(m.name.StartsWith("battery"))
                            {
                                m.SetTexture("_Illum", Utilities.GetTexture("Illum"));
                                m.SetColor("_GlowColor", Color.yellow);
                                m.SetFloat("_GlowStrength", 4f);
                            }
                        }
                    }
                }
            };

            prefab.SetGameObject(clone);
            prefab.SetUnlock(TechType.StasisRifle);
            prefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Tools);
            prefab.AddGadget(new EquipmentGadget(prefab, EquipmentType.Hand).WithQuickSlotType(QuickSlotType.Selectable));
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.StasisRifle, 1),
                new Ingredient(TechType.PlasteelIngot, 1),
                new Ingredient(TechType.AdvancedWiringKit, 1)))
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Resources", "BasicMaterials")
                .WithCraftingTime(0.5f);

            prefab.Register();
        }
    }
}