
using System.Collections;
using UnityEngine;
using UWE;

namespace Ramune.MoreDecoys.Handlers
{
    public class StasisHandler : MonoBehaviour
    {
        public static StasisSphere sphere;

        public void Start()
        {
            Subtitles.Add("Launching <color=#0f68ff>Creature stasis decoy</color>");
            CoroutineHost.StartCoroutine(DeployStasis());
        }

        public IEnumerator DeployStasis()
        {
            Helpers.SetTextures(gameObject, Helpers.stasisTexture, Helpers.stasisIllum, Helpers.stasisColor);

            sphere = StasisRifle.sphere;
            yield return new WaitForSecondsRealtime(7f);
            sphere.Shoot(transform.position, Helpers.quaternion, 0.1f, 5f, 10f);
            sphere.EnableField();
        }
    }
}