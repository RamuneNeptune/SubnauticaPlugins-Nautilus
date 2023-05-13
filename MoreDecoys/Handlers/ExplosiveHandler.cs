
using System.Collections;
using Nautilus.Utility;
using UnityEngine;
using UWE;

namespace Ramune.MoreDecoys.Items
{
    public class ExplosiveHandler : MonoBehaviour
    {
        public FMODAsset explosionSound = AudioUtils.GetFmodAsset("event:/sub/cyclops/explode");
        public GameObject explosionPrefab;

        public void Start()
        {
            Subtitles.Add("Launching <color=#993022>Creature explosive decoy</color>");
            CoroutineHost.StartCoroutine(DeployExplosive());
        }

        public IEnumerator DeployExplosive()
        {
            Helpers.SetTextures(gameObject, Helpers.explosiveTexture, Helpers.explosiveIllum, Helpers.explosiveColor);
            Subtitles.Add("Warning: High levels of explosive ordnance detected.");

            yield return new WaitForSecondsRealtime(7f);
            for(int i = 1; i <= 3; i++)
            {
                Explode();
                yield return new WaitForSecondsRealtime(7f);
            }
        }

        public void Explode()
        {
            FMODUWE.PlayOneShot(explosionSound, transform.position, volume: 0.7f);
            DamageSystem.RadiusDamage(300f, transform.position, 8f, DamageType.Explosive, gameObject);
            WorldForces.AddExplosion(transform.position, 5f, 5f, 10f);
        }
    }
}