﻿


namespace Ramune.RamunesOutcrops.Craftables
{
    public static class RadiantFins
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("RadiantFins", "Ultra Glide <color=#8f01ff>Radiant</color> Fins", "Streamline construction enhances swim speed considerably by comparison to regular fins.\n\nSPEED: +100%", Utilities.GetSprite("RadiantFinsSprite"), 2, 2);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.UltraGlideFins);

            prefab.SetGameObject(clone);
            prefab.SetUnlock(TechType.UltraGlideFins);
            prefab.SetEquipment(EquipmentType.Foots).WithQuickSlotType(QuickSlotType.None);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new Ingredient(TechType.UltraGlideFins, 1),
                new Ingredient(RadiantCrystal.Info.TechType, 2)))
                .WithFabricatorType(RadiantFabricator.CraftTreeType)
                .WithStepsToFabricatorTab("Equipment")
                .WithCraftingTime(0.5f);
            prefab.Register();
        }
    }
}