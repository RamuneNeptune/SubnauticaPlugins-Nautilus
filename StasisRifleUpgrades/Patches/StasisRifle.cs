using MK1 = Ramune.StasisRifleUpgrades.Items.StasisRifleMK1;
using MK2 = Ramune.StasisRifleUpgrades.Items.StasisRifleMK2;
using MK3 = Ramune.StasisRifleUpgrades.Items.StasisRifleMK3;
using static Ramune.StasisRifleUpgrades.Patches.Helpers;
using System.Collections.Generic;
using System.Collections;
using Nautilus.Utility;
using UnityEngine;
using HarmonyLib;
using RamuneLib;
using UWE;

#pragma warning disable

namespace Ramune.StasisRifleUpgrades.Patches
{
    public static class Helpers
    {
        public enum RifleType { MK1, MK2, MK3 }
        public static Color ColorDefault = new Color(128f / 255f, 143f / 255f, 255f / 255f);
        public static Color ColorMK1 = new Color(0f, 1f, 0f);
        public static Color ColorMK2 = new Color(0.7f, 0.6f, 0f);
        public static Color ColorMK3 = new Color(1f, 0f, 0f);
        public static float timeLastExploded;
        public static float cooldown = 15f;

        public static Dictionary<RifleType, (float, float, Color)> values = new Dictionary<RifleType, (float, float, Color)>()
        {
            { RifleType.MK1, (100f,  1.15f, ColorMK1) },
            { RifleType.MK2, (500f,  1.35f, ColorMK2) },
            { RifleType.MK3, (1000f, 1.65f, ColorMK3) },
        };
        public static GameObject explosion;


        public static void SetParticles(bool isRifle, Color color, StasisRifle rifle = null)
        {
            if(isRifle)
            {
                var vfxController = rifle.GetComponent<VFXController>();
                foreach(var particle in vfxController.emitters[0].instanceGO?.GetComponentsInChildren<ParticleSystem>()) particle.startColor = color;
                foreach(var particle in vfxController.emitters[0].fx?.GetComponentsInChildren<ParticleSystem>()) particle.startColor = color;
                foreach(var particle in vfxController.emitters[1].fx?.GetComponentsInChildren<ParticleSystem>()) particle.startColor = color;
                return;
            }
            else
            {
                var vfxController_ = StasisRifle.sphere.GetComponent<VFXController>();
                foreach (var particle in vfxController_.emitters[0].fx?.GetComponentsInChildren<ParticleSystem>()) particle.startColor = color;
                foreach (var particle in vfxController_.emitters[1].fx?.GetComponentsInChildren<ParticleSystem>()) particle.startColor = color;
            }
        }


        public static void Fire(StasisRifle rifle, float damage, float radius, Color color)
        {
            rifle.fxControl.Play(1);
            StasisRifle.sphere.startColor = color;
            FMODUWE.PlayOneShot(rifle.fireSound, rifle.tr.position, Mathf.Lerp(0.3f, 1f, rifle.chargeNormalized));
            StasisRifle.sphere.Shoot(rifle.muzzle.position, Player.main.camRoot.GetAimingTransform().rotation, 25f, 3f, rifle.chargeNormalized);
            rifle.chargeAmount = 0f;
            rifle.UpdateBar();
        }


        public static IEnumerator StasisSphereStuff(Color color, bool isMK3)
        {
            yield return new WaitUntil(() => StasisRifle.sphere.GetComponent<VFXController>().emitters[0] != null);
            SetParticles(false, color);

            if(!isMK3) yield break;

            if(Time.time < timeLastExploded)
            {
                float timeLeft = timeLastExploded - Time.time;
                Utilities.Log(Colors.Amber, "MK3 explosion is on cooldown for '" + timeLeft.Clamp01() + "' more seconds");
                yield break;
            }

            timeLastExploded = Time.time + cooldown;

            yield return new WaitForSecondsRealtime(1.5f);
            StasisRifleUpgrades.Explosion.transform.localScale = new Vector3(1f * StasisRifle.sphere.fieldRadius * 3f, 1f * StasisRifle.sphere.fieldRadius * 3f);
            Utils.PlayOneShotPS(StasisRifleUpgrades.Explosion, StasisRifle.sphere.transform.position, StasisRifle.sphere.transform.rotation, null);
            FMODUWE.PlayOneShot(AudioUtils.GetFmodAsset("event:/env/background/small_explode"), StasisRifle.sphere.transform.position, 1f);
            MainCameraControl.main.ShakeCamera(2f, 3f, MainCameraControl.ShakeMode.Quadratic, 1f);
        }
    }


    [HarmonyPatch(typeof(StasisRifle))]
    public static class RiflePatches
    {              
        [HarmonyPrefix]
        [HarmonyPatch(typeof(StasisRifle), nameof(StasisRifle.Charge))]
        public static void ChargePrefix(StasisRifle __instance)
        {
            var rifle = __instance.pickupable.GetTechType();
            if(__instance.chargeAmount <= 0f) return;
            if(rifle == TechType.StasisRifle) SetParticles(true, ColorDefault, __instance); else
            if(rifle == MK1.Info.TechType) SetParticles(true, ColorMK1, __instance); else
            if(rifle == MK2.Info.TechType) SetParticles(true, ColorMK2, __instance); else
            if(rifle == MK3.Info.TechType) SetParticles(true, ColorMK3, __instance);
            return;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(StasisRifle), nameof(StasisRifle.Fire))]
        public static bool FirePrefix(StasisRifle __instance, bool __runOriginal)
        {
            var rifle = __instance.pickupable.GetTechType();
            if(__instance.chargeAmount <= 0f) return false;

            if(rifle == TechType.StasisRifle)
            {
                StasisRifle.sphere.startColor = ColorDefault;
                CoroutineHost.StartCoroutine(StasisSphereStuff(ColorDefault, false));
                return true;
            };
            if(rifle == MK1.Info.TechType)
            {
                Fire(__instance, values[RifleType.MK1].Item1, values[RifleType.MK1].Item2, values[RifleType.MK1].Item3);
                CoroutineHost.StartCoroutine(StasisSphereStuff(values[RifleType.MK1].Item3, false));
            }
            else
            if(rifle == MK2.Info.TechType)
            {
                Fire(__instance, values[RifleType.MK2].Item1, values[RifleType.MK2].Item2, values[RifleType.MK2].Item3); 
                CoroutineHost.StartCoroutine(StasisSphereStuff(values[RifleType.MK2].Item3, false));
            }
            else
            if(rifle == MK3.Info.TechType)
            {
                Fire(__instance, values[RifleType.MK3].Item1, values[RifleType.MK3].Item2, values[RifleType.MK3].Item3);
                CoroutineHost.StartCoroutine(StasisSphereStuff(values[RifleType.MK3].Item3, true));
            }
            return false;
        }
    }
}