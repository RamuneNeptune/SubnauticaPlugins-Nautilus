using System.Collections;
using HarmonyLib;
using Nautilus.Utility;
using Ramune.StasisRifleUpgrades.Items;
using UnityEngine;
using UWE;

namespace Ramune.StasisRifleUpgrades.Patches
{
    [HarmonyPatch(typeof(StasisRifle))]
    public static class StasisRiflePatches
    {
        public static Color Default = new Color(128f/255f, 143f/255f, 255f/255f);
        public static Color MK1 = new Color(0.3f, 0.87f, 0.4f);
        public static Color MK2 = new Color(0.7f, 0.6f, 0f);
        public static Color MK3 = new Color(0.7f, 0.2f, 0.2f);

        public static float maxRadius = 10f;
        public static float maxTime = 20f;
        public static float damage;

        public enum RifleType { MK1, MK2, MK3 }
        public static TechType rifle;

        public static BasicText text = new BasicText();


        [HarmonyPostfix]
        [HarmonyPatch(typeof(StasisRifle), nameof(StasisRifle.chargeNormalized), MethodType.Getter)]
        public static void ChargePostfix(StasisRifle __instance, ref float __result)
        {
            rifle = __instance.pickupable.GetTechType();

            if(rifle == TechType.StasisRifle) return;
            else if(rifle == StasisRifleMK1.Info.TechType) __result = __instance.chargeAmount * 1.5f / __instance.energyCost;
            else if(rifle == StasisRifleMK2.Info.TechType) __result = __instance.chargeAmount * 3f / __instance.energyCost;
            else if(rifle == StasisRifleMK3.Info.TechType) __result = __instance.chargeAmount * 5f / __instance.energyCost;
        }


        [HarmonyPatch(typeof(StasisRifle), nameof(StasisRifle.Fire))]
        public static bool Prefix(StasisRifle __instance, bool __runOriginal)
        {
            text.SetFont(FontUtils.Aller_Rg);
            text.SetAlign(TMPro.TextAlignmentOptions.Midline);
            text.SetSize(25);
            
            rifle = __instance.pickupable.GetTechType();

            if(rifle == TechType.StasisRifle)
            {
                StasisRifle.sphere.startColor = Default;
                return true;
            }

            else if(rifle == StasisRifleMK1.Info.TechType) CoroutineHost.StartCoroutine(Fire(__instance, RifleType.MK1));
            else if(rifle == StasisRifleMK2.Info.TechType) CoroutineHost.StartCoroutine(Fire(__instance, RifleType.MK2));
            else if(rifle == StasisRifleMK3.Info.TechType) CoroutineHost.StartCoroutine(Fire(__instance, RifleType.MK3));

            return false;
        }

        public static IEnumerator Fire(StasisRifle instance, RifleType rifleType)
        {
            if(instance.chargeAmount <= 0f) yield break;

            if(rifleType == RifleType.MK1)
            {
                StasisRifle.sphere.startColor = MK1;
                damage = 100f;
            }
            else if(rifleType == RifleType.MK2)
            {
                damage = 500f;
                StasisRifle.sphere.startColor = MK2;
            }
            else if(rifleType == RifleType.MK3)
            {
                damage = 1000f;
                StasisRifle.sphere.startColor = MK3;
            }
            else yield break;

            instance.fxControl.Play(1);
            FMODUWE.PlayOneShot(instance.fireSound, instance.tr.position, Mathf.Lerp(0.3f, 1f, instance.chargeNormalized));
            StasisRifle.sphere.Shoot(instance.muzzle.position, Player.main.camRoot.GetAimingTransform().rotation, 25f, 3f, instance.chargeNormalized);
            instance.chargeAmount = 0f;
            instance.UpdateBar();

            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Crash);
            yield return task;
            var prefab = Object.Instantiate<GameObject>(task.GetResult());
            var crash = prefab.GetComponentInChildren<Crash>();
            var explosion = crash.detonateParticlePrefab;
            Object.Destroy(prefab);

            yield return new WaitUntil(() => StasisRifle.sphere.GetComponent<VFXController>().emitters[0].fx != null);
            SetParticles(StasisRifle.sphere.gameObject, 0, rifleType);

            yield return new WaitUntil(() => StasisRifle.sphere.GetComponent<VFXController>().emitters[1].fx != null);
            SetParticles(StasisRifle.sphere.gameObject, 1, rifleType);

            yield return new WaitUntil(() => instance.GetComponent<VFXController>().emitters[0].fx != null);
            SetParticles(instance.gameObject, 0, rifleType);

            yield return new WaitUntil(() => instance.GetComponent<VFXController>().emitters[1].fx != null);
            SetParticles(instance.gameObject, 1, rifleType);

            yield return new WaitUntil(() => StasisRifle.sphere.fieldEnabled);
            yield return new WaitForSecondsRealtime(1);
            explosion.transform.localScale = new Vector3(1f * StasisRifle.sphere.fieldRadius * 3f, 1f * StasisRifle.sphere.fieldRadius * 3f);
            Utils.PlayOneShotPS(explosion, StasisRifle.sphere.transform.position, StasisRifle.sphere.transform.rotation, null);
            FMODUWE.PlayOneShot(AudioUtils.GetFmodAsset("event:/env/background/small_explode"), StasisRifle.sphere.transform.position, 1.7f);
            MainCameraControl.main.ShakeCamera(2f, 3f, MainCameraControl.ShakeMode.Quadratic, 1f);

            while(StasisRifle.sphere.fieldEnabled)
            {
                foreach(var rigid in StasisRifle.sphere.targets)
                {
                    if(!rigid.gameObject.GetComponentInChildren<LiveMixin>().IsAlive()) yield return null;
                    rigid.gameObject.GetComponentInChildren<LiveMixin>().TakeDamage(10f);
                }
                text.ShowMessage($"---  Applying '{damage}' damage per second for {rifleType} ---", 1.2f);
                yield return new WaitForSecondsRealtime(1f);
            }
            yield break;
        }

        public static void SetParticles(GameObject gameObject, int num, RifleType rifleType)
        {
            if(rifleType == RifleType.MK1) foreach(var p in gameObject.GetComponent<VFXController>().emitters[num].fx.GetComponentsInChildren<ParticleSystem>()) p.startColor = Color.green;
            if(rifleType == RifleType.MK2) foreach(var p in gameObject.GetComponent<VFXController>().emitters[num].fx.GetComponentsInChildren<ParticleSystem>()) p.startColor = Color.yellow;
            if(rifleType == RifleType.MK3) foreach(var p in gameObject.GetComponent<VFXController>().emitters[num].fx.GetComponentsInChildren<ParticleSystem>()) p.startColor = Color.red;
        }
    }
}