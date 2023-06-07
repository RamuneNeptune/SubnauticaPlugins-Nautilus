


namespace Ramune.RamunesOutcrops.Craftables
{
    public static class RadiantCube
    {
        public static PrefabInfo Info;

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("RadiantCube", "<color=#8f01ff>Radiant</color> Cube", "High capacity energy source.", Utilities.GetSprite("RadiantCubeSprite"), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.PrecursorIonCrystal)
            {
                ModifyPrefab = go =>
                {
                    go.FindChild("Point light").GetComponent<Light>().color = new Color(0.72f, 0f, 0.85f);

                    var renderers = go.GetComponentsInChildren<MeshRenderer>(true);
                    foreach(var r  in renderers)
                    {
                        foreach(var m in r.materials)
                        {
                            m.mainTexture = Utilities.GetTexture("RadiantCubeTexture");
                            m.SetTexture("_Illum", Utilities.GetTexture("RadiantCubeTexture"));
                            m.SetTexture("_SpecTex", Utilities.GetTexture("RadiantCubeTexture"));
                            m.SetColor("_GlowColor", new Color(0.67f, 0.1f, 0.85f, 0.4f));
                            m.color = new Color(0.5f, 0f, 0.5f);
                        }
                    }
                    go.EnsureComponent<Battery>()._capacity = 300000;
                    PrefabUtils.AddVFXFabricating(go, "", 1.5f, 1.5f);
                }
            };

            prefab.SetUnlock(TechType.PrecursorIonCrystal);
            prefab.SetEquipment(EquipmentType.None).WithQuickSlotType(QuickSlotType.Instant);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.PrecursorIonCrystal, 1),
                new Ingredient(RadiantCrystal.Info.TechType, 1)))
                .WithFabricatorType(RadiantFabricator.CraftTreeType)
                .WithStepsToFabricatorTab("Electronics")
                .WithCraftingTime(0.5f);
            prefab.SetGameObject(clone);
            prefab.Register();
        }   
    }
}