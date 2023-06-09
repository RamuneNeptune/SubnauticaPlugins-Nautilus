


namespace Ramune.RamunesOutcrops.Buildables
{
    public static class RadiantWallLocker
    {
        public static PrefabInfo Info;

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("RadiantWallLocker", "Radiant wall locker", "A radiant wall locker.", Utilities.GetSprite(TechType.SmallLocker), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.SmallLocker)
            {
                ModifyPrefab = locker =>
                {
                    var storage = locker.GetComponent<StorageContainer>();
                    storage.hoverText = "Open radiant locker";
                    storage.storageLabel = "Radiant wall locker";
                    storage.height = 4;
                    storage.width = 4;
                    storage.Resize(4, 4);

                    MeshRenderer[] renderers = locker.GetComponentsInChildren<MeshRenderer>();
                    foreach(var r in renderers)
                    {
                        if(r.gameObject.name == "submarine_locker_02")
                        {
                            r.materials[0].SetTexture("_MainTex", Utilities.GetTexture("RadiantWallLockerTexture2"));
                            r.materials[0].SetTexture("_SpecTex", Utilities.GetTexture("RadiantWallLockerTexture2"));
                            r.materials[1].SetTexture("_MainTex", Utilities.GetTexture("RadiantWallLockerTexture1"));
                            r.materials[1].SetTexture("_SpecTex", Utilities.GetTexture("RadiantWallLockerTexture1"));
                            r.materials[1].SetTexture("_Illum", Utilities.GetTexture("RadiantWallLockerTexture3"));
                            r.materials[0].SetTexture("_Illum", Utilities.GetTexture("RadiantWallLockerTexture3"));
                            r.materials[1].SetColor("_GlowColor", new Color(0.49f, 0f, 0.77f, 0.4f));
                            r.materials[0].SetColor("_GlowColor", new Color(0.49f, 0f, 0.77f, 0.4f));
                            r.materials[1].SetFloat("_GlowStrength", 3.2f);
                            r.materials[0].SetFloat("_GlowStrength", 3.2f);
                            r.materials[1].EnableKeyword("MARMO_EMISSION");
                            r.materials[0].EnableKeyword("MARMO_EMISSION");
                        }
                        else
                        {
                            r.material.SetTexture("_MainTex", Utilities.GetTexture("RadiantWallLockerTexture1"));
                            r.material.SetTexture("_SpecTex", Utilities.GetTexture("RadiantWallLockerTexture1"));
                        }
                    }
                }
            };

            prefab.SetGameObject(clone);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule).SetBuildable(true);
            prefab.SetUnlock(RadiantCrystal.Info.TechType);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.Quartz, 1),
                new Ingredient(TechType.Titanium, 2),
                new Ingredient(RadiantCrystal.Info.TechType, 1)));

            prefab.Register();
        }
    }
}