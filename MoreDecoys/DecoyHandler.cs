
using System.Collections;
using Nautilus.Utility;
using RamuneLib.Utils;
using UnityEngine;
using Texture = RamuneLib.Utils.Texture;
using Sound = FMOD.Sound;
using Nautilus.Handlers;

namespace Ramune.MoreDecoys
{
    public class DecoyHandler : MonoBehaviour
    {
        public StasisSphere sphere;
        public GameObject explosionPrefab;
        public Texture2D decoyStasisTexture = Texture.Get("DecoyStasis_tex");
        public FMODAsset explosion = AudioUtils.GetFmodAsset("event:/sub/cyclops/explode");
        public Quaternion quaternion = new Quaternion(1f, 1f, 1f, 1f);
        public int id;

        public void Start() 
        {
            if(id == 1) StartCoroutine(DeployStasis());
            else if(id == 2) StartCoroutine(DeployExplosive());
            else if(id == 3) StartCoroutine(DeployGas());
            else MoreDecoys.logger.LogError("DecoyHandler could not find valid 'id'");
        }

        public IEnumerator DeployStasis()
        {
            sphere = StasisRifle.sphere; 
            yield return new WaitForSecondsRealtime(7f);
            sphere.Shoot(transform.position, quaternion, 0.1f, 5f, 10f); 
            sphere.EnableField();
        }

        public IEnumerator DeployExplosive()
        {
            yield return new WaitForSecondsRealtime(7f);
            for(int i = 1; i <= 3; i++) 
            {
                Explode();
                Log.Colored(Colors.Red, $"{i}");
                yield return new WaitForSecondsRealtime(7f);
            }
        }

        public void Explode()
        {
            FMODUWE.PlayOneShot(explosion, transform.position, volume: 0.8f);
            DamageSystem.RadiusDamage(1000f, transform.position, 25f, DamageType.Explosive, gameObject);
            WorldForces.AddExplosion(transform.position, 5f, 5f, 10f);
        }

        public IEnumerator DeployGas()
        {
            Log.Colored(Colors.Lime, "Gas deploying in 7 seconds...");
            GasPod pod = new GasPod();
            pod.damageRadius = 20f;
            pod.damagePerSecond = 16f;
            pod.damageInterval = 0.5f;
            pod.smokeDuration = 30f;

            yield return new WaitForSecondsRealtime(7f);

            Log.Colored(Colors.Lime, "Gas deployed!");
        }
    }
}