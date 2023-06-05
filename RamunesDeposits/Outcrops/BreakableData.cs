


namespace Ramune.RamunesOutcrops.Outcrops
{
    public static class BreakableData
    {
        public static List<BreakableResource.RandomPrefab> Lodestone = new()
        {
            new() { prefabReference = new AssetReferenceGameObject("e95249a7366f10c4dbb167d7d83a6f50"), prefabTechType = TechType.Lithium, chance = 0.55f },
            new() { prefabReference = new AssetReferenceGameObject("215f50ec86e2fdc4fa2887097e30030d"), prefabTechType = TechType.Titanium, chance = 0.45f }
        };

        public static List<BreakableResource.RandomPrefab> Geyserite = new()
        {
            new() { prefabReference = new AssetReferenceGameObject("899208a808b92ef42909fc3ec6651a90"), prefabTechType = TechType.Lead, chance = 0.54f },
            new() { prefabReference = new AssetReferenceGameObject("215f50ec86e2fdc4fa2887097e30030d"), prefabTechType = TechType.Titanium, chance = 0.46f }
        };

        public static List<BreakableResource.RandomPrefab> Siltstone = new()
        {
            new() { prefabReference = new AssetReferenceGameObject("3676596b5e495456ba5f887c8768649d"), prefabTechType = TechType.Copper, chance = 0.15f },
            new() { prefabReference = new AssetReferenceGameObject("b3b8aabaa22aa1b48a7d69a7517cabde"), prefabTechType = TechType.Silver, chance = 0.15f },
            new() { prefabReference = new AssetReferenceGameObject("e95249a7366f10c4dbb167d7d83a6f50"), prefabTechType = TechType.Lithium, chance = 0.35f },
            new() { prefabReference = new AssetReferenceGameObject("3676596b5e495456ba5f887c8768649d"), prefabTechType = TechType.Titanium, chance = 0.35f },
        };

        public static List<BreakableResource.RandomPrefab> Serpentite = new()
        {
            new() { prefabReference = new AssetReferenceGameObject("3676596b5e495456ba5f887c8768649d"), prefabTechType = TechType.Titanium, chance = 0.5f },
            new() { prefabReference = new AssetReferenceGameObject("e95249a7366f10c4dbb167d7d83a6f50"), prefabTechType = TechType.Lithium, chance = 0.5f },
        };
    }
}