


namespace Ramune.RamunesOutcrops.Craftables
{
    public class RadiantLight : MonoBehaviour
    {
        public Light light;
        public float duration;

        public void Start()
        {
            duration = 3f;
            light = gameObject.EnsureComponent<Light>();
            light.name = "RadiantCrystal";
            light.range = 1.5f;
            light.intensity = 2f;
            light.color = new Color(0.75f, 0f, 1f);
        }

        public void Update()
        {

        }
        /*
        public IEnumerator FadeIntensity()
        {
            float timeStarted = Time.time;
            while(Time.time - timeStarted <= duration)
            {
                float t = Time.time - timeStarted / duration;
                float lerpedValue = Mathf.Lerp(2f, 1f, t);
            }
        }
        */
    }


    public static class RadiantCrystal
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("RadiantCrystal", "<color=#8f01ff>Radiant</color> crystal", "A piece of radiant crystal.", Utilities.GetSprite("RadiantCrystalSprite"), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.Kyanite)
            {
                ModifyPrefab = go =>
                {
                    go.EnsureComponent<RadiantLight>();
                    var renderers = go.GetComponentsInChildren<MeshRenderer>(true);
                    foreach(var r in renderers)
                    {
                        foreach(var m in r.materials)
                        {
                            m.mainTexture = Utilities.GetTexture("RadiantCrystalTexture");
                            m.SetTexture("_SpecTex", Utilities.GetTexture("RadiantCrystalSpecTexture"));
                            m.SetTexture("_Illum", Utilities.GetTexture("RadiantCrystalTexture"));
                            m.SetColor("_Illum", new Color(0.67f, 0.1f, 0.85f));
                            m.color = new Color(0.67f, 0.1f, 0.85f);
                            //MaterialUtils.SetMaterialTransparent(m, true);
                        }
                    }
                }
            };
            prefab.SetGameObject(clone);
            prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);
            prefab.SetSpawns(BiomeData.RadiantCrystal.ToArray());
            prefab.Register();
        }
    }
}