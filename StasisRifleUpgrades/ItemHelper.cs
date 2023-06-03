
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using RamuneLib;
using UnityEngine;
using Nautilus.Assets.Gadgets;
using static CraftData;

namespace Ramune.StasisRifleUpgrades.Items
{
    public static class ItemHelper
    {
        public static Texture2D StasisTex = Utilities.GetTexture("Stasis");
        public static Texture2D StasisIllumTex = Utilities.GetTexture("StasisIllum");
        public static Texture2D BatteryIllumTex = Utilities.GetTexture("BatteryIllum");
        public static Texture2D BarOnTex = Utilities.GetTexture("BarOn");
        public static Texture2D BarOffTex = Utilities.GetTexture("BarOff");

        public static PrefabInfo CreatePrefabInfo(int mark)
        {
            return Utilities.CreatePrefabInfo("StasisRifleMK" + mark, "Stasis rifle MK" + mark, "A mega stasis rifle cool wow yeah", Utilities.GetSprite("MK" + mark), 2, 2);
        }

        public static void CreateRifle(int mark, PrefabInfo prefabInfo, Color glowColor)
        {
            var prefab = new CustomPrefab(prefabInfo);

            prefab.SetUnlock(TechType.StasisRifle);
            prefab.SetPdaGroupCategory(TechGroup.Workbench, TechCategory.Workbench);
            prefab.SetEquipment(EquipmentType.Hand).WithQuickSlotType(QuickSlotType.Selectable);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.StasisRifle, 1),
                new Ingredient(TechType.PlasteelIngot, 1),
                new Ingredient(TechType.AdvancedWiringKit, 1)))
                .WithFabricatorType(CraftTree.Type.Workbench)
                .WithStepsToFabricatorTab("StasisRifles")
                .WithCraftingTime(0.5f);

            var clone = new CloneTemplate(prefabInfo, TechType.StasisRifle)
            {
                ModifyPrefab = go =>
                {
                    var renderers = go.GetComponentsInChildren<SkinnedMeshRenderer>(true);

                    foreach(var r in renderers)
                    {
                        var materials = r.materials;
                        var materialsLength = materials.Length;

                        for(int i = 0; i < materialsLength; i++)
                        {
                            switch(materials[i].name)
                            {
                                case var name when name.StartsWith("stasis"):
                                    materials[i].mainTexture = StasisTex;
                                    materials[i].SetTexture("_SpecTex", StasisTex);
                                    materials[i].SetTexture("_Illum", StasisIllumTex);
                                    materials[i].SetColor("_GlowColor", glowColor);
                                    materials[i].SetFloat("_GlowStrength", 1f);
                                    materials[i].EnableKeyword("MARMO_EMISSION");
                                    break;

                                case var name when name.StartsWith("battery"):
                                    materials[i].SetTexture("_Illum", BatteryIllumTex);
                                    materials[i].SetColor("_GlowColor", glowColor);
                                    materials[i].SetFloat("_GlowStrength", 4f);
                                    materials[i].color = Color.black;
                                    break;

                                case var name when name.StartsWith("UI"):
                                    materials[i].SetTexture("_MainTex", BarOffTex);
                                    materials[i].SetColor("_MainTex", glowColor);
                                    materials[i].SetTexture("_BarTex", BarOnTex);
                                    materials[i].SetColor("_BarTex", glowColor);
                                    materials[i].color = glowColor;
                                    break;
                            }
                        }
                    }
                }
            };
            prefab.SetGameObject(clone);
            prefab.Register();
        }
    }
}