

namespace Ramune.RadiantResources.Monos
{
    public class RadiantAreaLight : MonoBehaviour
    {
        public Light light;
        public Color color = new(0.75f, 0f, 1f);

        public void SetupLight(float range = 1.5f, float intensity = 2f)
        {
            GameObject lightRoot = new()
            {
                name = "LightRoot",
            };

            lightRoot.transform.parent = gameObject.transform;

            light = gameObject.EnsureComponent<Light>();
            light.intensity = intensity;
            light.range = range;
            light.color = color;
            light.name = "RadiantAreaLight";
        }
    }
}