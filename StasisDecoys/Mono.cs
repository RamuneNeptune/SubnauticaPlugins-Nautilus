
using System.Collections;
using System.IO;
using System.Reflection;
using FMOD;
using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Utility;
using RamuneLib.Utils;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Sound = RamuneLib.Utils.Sound;

namespace Ramune.MoreDecoys
{
    public class DecoyHandler : MonoBehaviour
    {
        public StasisSphere sphere;
        public Quaternion quaternion = new Quaternion(1f, 1f, 1f, 1f);
        public FMOD.Sound explosion;
        public GameObject explosionPrefab;
        public int id;

        public void Start() // Runs once after a DecoyHandler is created/added to a decoy
        {
            if(id == 1)
            {
                StartCoroutine(DeployStasis()); // IEnumerators have to be started with 'StartCoroutine(Method());'
            }
            else if(id == 2)
            {
                explosion = Sound.Create("DecoyExplosive");
                StartCoroutine(DeployExplosive());
            }
            else MoreDecoys.logger.LogError("DecoyHandler could not find valid 'id'");
        }

        // IEnumerators let you wait for things to be ready before continuing, e.g. waiting till a MeshRenderer exists, or waiting in seconds and such
        public IEnumerator DeployStasis()
        {
            // Just so I can type `sphere` rather than `StasisRifle.sphere`
            sphere = StasisRifle.sphere;

            // While the MeshRenderer is null, return
            while (gameObject.GetComponentInChildren<MeshRenderer>() == null) yield return null; 

            // MeshRender is available, now we make changes using with
            MeshRenderer renderer = gameObject.GetComponentInChildren<MeshRenderer>();
            renderer.material.mainTexture = ImageUtils.LoadTextureFromFile(Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets"), "DecoyStasis"));

            // Delay before deploying the stasis field
            yield return new WaitForSecondsRealtime(3f);
            sphere.Shoot(transform.position, quaternion, 0.1f, 5f, 10f); 
            sphere.EnableField(); // Shoot first, then instantly deploy the field (so it's centered on the decoy rather than shooting off a billion lightyears away)
        }

        public IEnumerator DeployExplosive()
        {
            GameObject prefab;
            var task = CraftData.GetPrefabForTechTypeAsync(TechType.WhirlpoolTorpedo);
            yield return task;
            prefab = task.GetResult();
            if(prefab != null) prefab.GetComponentInChildren<SeamothTorpedo>().explosionPrefab = explosionPrefab;

            yield return new WaitForSecondsRealtime(3f);
            WorldForces.AddExplosion(transform.position, 3f, 3f, 10f);
            // I just ripped code from the Crashfish (explodey guy), the Crashfish class in the game is literally called 'Crash'...
            DamageSystem.RadiusDamage(200f, transform.position, 15f, DamageType.Explosive, gameObject); 
            Sound.Play(explosion, AudioUtils.BusPaths.UnderwaterAmbient);
        }
    }
}