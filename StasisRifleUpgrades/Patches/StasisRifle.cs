using System.Collections;
using HarmonyLib;
using Nautilus.Utility;
using Ramune.StasisRifleUpgrades.Items;
using UnityEngine;
using UWE;

namespace Ramune.StasisRifleUpgrades.Patches
{
    [HarmonyPatch(typeof(StasisRifle), nameof(StasisRifle.Fire))]
    public static class StasisRiflePatch
    {
        public static Color Default = new Color(128f/255f, 143f/255f, 255f/255f);
        public static Color MK1 = new Color(0.3f, 0.87f, 0.4f);
        public static Color MK2 = new Color(0.7f, 0.6f, 0f);
        public static Color MK3 = new Color(0.7f, 0.2f, 0.2f);

        public static float maxRadius = 10f;
        public static float maxTime = 20f;

        public enum RifleType { MK1, MK2, MK3 }
        public static TechType rifle;

        public static BasicText text = new BasicText();

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
            if(instance.chargeAmount <= 0f) yield return null;

            if(rifleType == RifleType.MK1) StasisRifle.sphere.startColor = MK1;
            else if(rifleType == RifleType.MK2) StasisRifle.sphere.startColor = MK2;
            else if(rifleType == RifleType.MK3) StasisRifle.sphere.startColor = MK3;
            else yield return null;

            instance.fxControl.Play(1);
            FMODUWE.PlayOneShot(instance.fireSound, instance.tr.position, Mathf.Lerp(0.3f, 1f, instance.chargeNormalized));
            StasisRifle.sphere.Shoot(instance.muzzle.position, Player.main.camRoot.GetAimingTransform().rotation, 25f, 3f, instance.chargeNormalized);
            instance.chargeAmount = 0f;
            instance.UpdateBar();
            yield return new WaitUntil(() => StasisRifle.sphere.fieldEnabled);

            while(StasisRifle.sphere.fieldEnabled)
            {

                foreach(var rigid in StasisRifle.sphere.targets)
                {
                    if(!rigid.gameObject.GetComponentInChildren<LiveMixin>().IsAlive()) yield return null;
                    rigid.gameObject.GetComponentInChildren<LiveMixin>().TakeDamage(10f);
                }
                text.ShowMessage($"---  Applying '10' damage per second for {rifleType} ---");
                yield return new WaitForSecondsRealtime(1f);
            }
            yield return null;
        }
    }
}