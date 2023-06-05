


namespace Ramune.RamunesOutcrops.Craftables
{
    public class RadiantThermobladeMono : HeatBlade
    {
        public void Start()
        {
            attackDist = 100f;
            damage = 10000f;
        }

        public override void OnToolUseAnim(GUIHand hand)
        {
            ErrorMessage.AddError("Knife used to attack");
            base.OnToolUseAnim(hand);
        }
    }

    public static class RadiantThermoblade
    {
        public static Texture2D RadiantBladeTexture = Utilities.GetTexture("RadiantBladeTexture");
        public static Texture2D RadiantBladeIllumTexture = Utilities.GetTexture("RadiantBladeIllumTexture");

        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("RadiantHeatBlade", "<color=#C858DF>Radiant</color> Thermoblade", "Cooks and sterilizes small organisms for immediate consumption.\n\nDAMAGE: +75%\nRANGE: +30%", Utilities.GetSprite("RadiantBladeSprite"), 1, 1);
            var prefab = new CustomPrefab(Info);

            prefab.SetUnlock(TechType.HeatBlade);
            prefab.SetEquipment(EquipmentType.Hand).WithQuickSlotType(QuickSlotType.Selectable);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.HeatBlade, 1),
                new Ingredient(RadiantCrystal.Info.TechType, 2)))
                .WithFabricatorType(RadiantFabricator.CraftTreeType)
                .WithStepsToFabricatorTab("Tools")
                .WithCraftingTime(0.5f);

            var clone = new CloneTemplate(Info, TechType.HeatBlade)
            {
                ModifyPrefab = go =>
                {
                    var renderer = go.GetComponentInChildren<MeshRenderer>(true);
                    foreach(var m in renderer.materials)
                    {
                        m.mainTexture = RadiantBladeIllumTexture;
                        m.SetTexture("_SpecTex", RadiantBladeIllumTexture);
                        m.SetTexture("_Illum", RadiantBladeIllumTexture);
                        m.SetColor("_GlowColor", new Color(0.67f, 0.1f, 0.85f, 0.4f));
                    }

                    var heatblade = go.GetComponent<HeatBlade>();
                    var radiantblade = go.EnsureComponent<RadiantThermobladeMono>().CopyComponent(heatblade);

                    UnityEngine.Object.DestroyImmediate(heatblade);
                }
            };
            prefab.SetGameObject(clone);
            prefab.Register();
        }
    }
}