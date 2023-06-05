


namespace Ramune.RamunesOutcrops.Craftables
{
    public static class RadiantCrystal
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("RadiantCrystal", "<color=#C858DF>Radiant</color> crystal", "A piece of radiant crystal.", Utilities.GetSprite("RadiantCrystalSprite"), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.Kyanite)
            {
                ModifyPrefab = go =>
                {
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
                            MaterialUtils.SetMaterialTransparent(m, true);
                        }
                    }
                }
            };
            prefab.SetGameObject(clone);
            prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);
            prefab.SetSpawns(Helpers.CreateBiomeData(BiomeData.RadiantCrystal));
            prefab.Register();
        }
    }
}