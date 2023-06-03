
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using static CraftData;
using RamuneLib;
using Nautilus.Assets.Gadgets;
using UnityEngine;
using Ramune.RamunesOutcrops.Fabricators;
using System.Collections;
using UWE;
using UnityEngine.XR;

namespace Ramune.RamunesOutcrops.Items
{
    public class RadiantThermobladeMono : HeatBlade
    {
        public VFXController fxControl;
        public FMOD_CustomEmitter idleClip;

        public void Start()
        {
            CoroutineHost.StartCoroutine(FetchFX());
            attackDist = 100f;
            damage = 10000f;
        }

        public IEnumerator FetchFX()
        {
            var task = GetPrefabForTechTypeAsync(TechType.HeatBlade);
            yield return task;

            var result = task.GetResult();
            var hb = result.GetComponent<HeatBlade>();

            fxControl = hb.fxControl;
            idleClip = hb.idleClip;

            yield break;
        }

        public override int GetUsesPerHit()
        {
            return 1;
        }

        public override void OnDraw(Player p)
        {
            idleClip.Play();
            fxControl.Play();
            base.OnDraw(p);
        }

        public override void OnHolster()
        {
            idleClip.Stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            fxControl.StopAndDestroy(0f);
            base.OnHolster();
        }

        public override void OnToolUseAnim(GUIHand hand)
        {
            ErrorMessage.AddError("Used radiant thermoblade to hit a resource");
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
                .WithCraftingTime(0.5f);

            var clone = new CloneTemplate(Info, TechType.HeatBlade)
            {
                ModifyPrefab = go =>
                {
                    var knife = go.EnsureComponent<RadiantThermobladeMono>();
                    var renderer = go.GetComponentInChildren<MeshRenderer>(true);
                    foreach(var m in renderer.materials)
                    {
                        m.mainTexture = RadiantBladeIllumTexture;
                        m.SetTexture("_SpecTex", RadiantBladeIllumTexture);
                        m.SetTexture("_Illum", RadiantBladeIllumTexture);
                        m.SetColor("_GlowColor", new Color(0.67f, 0.1f, 0.85f, 0.4f));
                    }
                }
            };
            prefab.SetGameObject(clone);
            prefab.Register();
        }
    }
}