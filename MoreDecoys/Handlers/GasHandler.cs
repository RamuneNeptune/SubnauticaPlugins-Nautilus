
using System.Collections;
using UnityEngine;
using UWE;

namespace Ramune.MoreDecoys.Handlers
{
    public class GasHandler : MonoBehaviour
    {
        public void Start()
        {
            Subtitles.Add("Launching <color=#7dbf43>Creature gas decoy</color>");
            CoroutineHost.StartCoroutine(DeployGas());
        }

        public IEnumerator DeployGas()
        {
            Helpers.SetTextures(gameObject, Helpers.gasTexture, Helpers.gasIllum, Helpers.gasColor);

            var task = CraftData.GetPrefabForTechTypeAsync(TechType.GasPod, false);
            yield return task;

            var prefab = task.GetResult();
            var go = Instantiate(prefab);
            var gas = go.GetComponentInChildren<GasPod>();

            gas.damageRadius = 15f;
            gas.damagePerSecond = 20f;
            gas.damageInterval = 0.5f;
            gas.smokeDuration = 45f;
            gas.autoDetonateTime = 20f;

            yield return new WaitForSecondsRealtime(7f);
            gas.transform.position = transform.position;
            gas.Detonate();
        }
    }
}