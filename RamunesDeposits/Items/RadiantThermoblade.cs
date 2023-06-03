
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
        public void Start()
        {
            var hb = gameObject.GetComponent<HeatBlade>();
            idleClip = hb.idleClip;
            fxControl = hb.fxControl;
            attackSound = hb.attackSound;
            surfaceMissSound = hb.surfaceMissSound;
            underwaterMissSound = hb.underwaterMissSound;
            vfxEventType = hb.vfxEventType;
            bleederDamage = hb.bleederDamage + 20f;
            drawSound = hb.drawSound;
            drawTime = hb.drawTime;
            dropTime = hb.dropTime;
            hasBashAnimation = hb.hasBashAnimation;
            hitBleederSound = hb.hitBleederSound;
            holsterTime = hb.holsterTime;
            mainCollider = hb.mainCollider;
            pickupable = hb.pickupable;
            savedIkAimRightArm = hb.savedIkAimRightArm;
            usingPlayer = hb.usingPlayer;
            attackDist = 100f;
            damage = 10000f;
            Destroy(hb);
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