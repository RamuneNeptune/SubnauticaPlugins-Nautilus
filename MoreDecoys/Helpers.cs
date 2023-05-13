
using UnityEngine;
using Texture = RamuneLib.Utilities.Texture;

#pragma warning disable CS0618 // Type or member is obsolete

namespace Ramune.MoreDecoys
{
    public class Helpers : MonoBehaviour
    {
        public static Quaternion quaternion = new Quaternion(1f, 1f, 1f, 1f);

        public static Texture2D stasisTexture = Texture.Get("DecoyStasis_tex");
        public static Texture2D stasisIllum = Texture.Get("DecoyStasis_illum");
        public static Color stasisColor = new Color(76f, 129f, 190f, 1f);

        public static Texture2D explosiveTexture = Texture.Get("DecoyExplosive_tex");
        public static Texture2D explosiveIllum = Texture.Get("DecoyExplosive_illum");
        public static Color explosiveColor = new Color(178f, 24f, 0f, 1f);

        public static Texture2D gasTexture = Texture.Get("DecoyGas_tex");
        public static Texture2D gasIllum = Texture.Get("DecoyGas_illum");
        public static Color gasColor = new Color(49f, 153f, 79f, 1f);


        public static void SetTextures(GameObject go, Texture2D texture, Texture2D illum, Color flareColor)
        {
            var renderer = go.GetComponentInChildren<MeshRenderer>(true);
            if(renderer != null)
            {
                renderer.material.mainTexture = texture;
                renderer.material.SetTexture("_SpecTex", texture);
                renderer.material.SetTexture("_Illum", illum);
            }
            var particles = go.GetComponentsInChildren<ParticleSystem>();
            if(particles != null)
            {
                foreach (var particle in particles)
                {
                    if(particle.name != "xFlare") break;
                    particle.startColor = flareColor;
                }
            }
        }
    }
}