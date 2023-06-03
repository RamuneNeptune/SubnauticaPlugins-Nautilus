using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Ramune.RamunesOutcrops
{
    public static class Resources
    {
        public static List<BreakableResource.RandomPrefab> LodestoneOutcropItems = new List<BreakableResource.RandomPrefab>()
        {
            new BreakableResource.RandomPrefab{ prefabReference = new AssetReferenceGameObject("e95249a7366f10c4dbb167d7d83a6f50"), prefabTechType = TechType.Lithium, chance = 0.55f },
            new BreakableResource.RandomPrefab{ prefabReference = new AssetReferenceGameObject("215f50ec86e2fdc4fa2887097e30030d"), prefabTechType = TechType.Titanium, chance = 0.45f }
        };

        public static Dictionary<BiomeType, (int, float)> LodestoneOutcropBiomes = new Dictionary<BiomeType, (int, float)>()   
        {
            { BiomeType.JellyshroomCaves_CaveFloor,   (1, 1f) },
            { BiomeType.JellyshroomCaves_CaveWall,    (1, 1f) },
            { BiomeType.JellyshroomCaves_CaveCeiling, (1, 1f) },
        };



        public static List<BreakableResource.RandomPrefab> GeyseriteOutcropItems = new List<BreakableResource.RandomPrefab>()
        {
            new BreakableResource.RandomPrefab{ prefabReference = new AssetReferenceGameObject("899208a808b92ef42909fc3ec6651a90"), prefabTechType = TechType.Lead, chance = 0.54f },
            new BreakableResource.RandomPrefab{ prefabReference = new AssetReferenceGameObject("215f50ec86e2fdc4fa2887097e30030d"), prefabTechType = TechType.Titanium, chance = 0.46f }
        };

        public static Dictionary<BiomeType, (int, float)> GeyseriteOutcropBiomes = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.Dunes_ThermalVent,              (1, 1f) },
            { BiomeType.BonesField_ThermalVent,         (1, 1f) },
            { BiomeType.GrandReef_ThermalVent,          (1, 1f) },
            { BiomeType.DeepGrandReef_ThermalVent,      (1, 1f) },
            { BiomeType.LostRiverCorridor_ThermalVents, (1, 1f) },
            { BiomeType.LostRiverJunction_ThermalVent,  (1, 1f) },
            { BiomeType.Mountains_ThermalVent,          (1, 1f) },
        };


        public static List<BreakableResource.RandomPrefab> SiltstoneOutcropItems = new List<BreakableResource.RandomPrefab>()
        {   
            new BreakableResource.RandomPrefab{ prefabReference = new AssetReferenceGameObject("3676596b5e495456ba5f887c8768649d"), prefabTechType = TechType.Copper, chance = 0.15f },
            new BreakableResource.RandomPrefab{ prefabReference = new AssetReferenceGameObject("b3b8aabaa22aa1b48a7d69a7517cabde"), prefabTechType = TechType.Silver, chance = 0.15f },
            new BreakableResource.RandomPrefab{ prefabReference = new AssetReferenceGameObject("e95249a7366f10c4dbb167d7d83a6f50"), prefabTechType = TechType.Lithium, chance = 0.35f },
            new BreakableResource.RandomPrefab{ prefabReference = new AssetReferenceGameObject("3676596b5e495456ba5f887c8768649d"), prefabTechType = TechType.Titanium, chance = 0.35f },
        };

        public static Dictionary<BiomeType, (int, float)> SiltstoneOutcropBiomes = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.MushroomForest_GiantTreeInteriorFloor, (1, 1f) },
            { BiomeType.MushroomForest_MushroomTreeTrunk,      (1, 1f) },
            { BiomeType.MushroomForest_CaveCeiling,            (1, 1f) },
            { BiomeType.MushroomForest_CaveFloor,              (1, 1f) },
            { BiomeType.MushroomForest_RockWall,               (1, 1f) },
            { BiomeType.MushroomForest_CaveWall,               (1, 1f) },
            { BiomeType.MushroomForest_Sand,                   (1, 1f) },
        };



        public static List<BreakableResource.RandomPrefab> SerpentiteOutcropItems = new List<BreakableResource.RandomPrefab>()
        {
            new BreakableResource.RandomPrefab{ prefabReference = new AssetReferenceGameObject("3676596b5e495456ba5f887c8768649d"), prefabTechType = TechType.Titanium, chance = 0.5f },
            new BreakableResource.RandomPrefab{ prefabReference = new AssetReferenceGameObject("e95249a7366f10c4dbb167d7d83a6f50"), prefabTechType = TechType.Lithium, chance = 0.5f },
        };

        public static Dictionary<BiomeType, (int, float)> SerpentiteOutcropBiomes = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.LostRiverCorridor_Ceiling, (1, 1f) },
            { BiomeType.LostRiverCorridor_Wall,    (1, 1f) },
            { BiomeType.LostRiverJunction_Ceiling, (1, 1f) },
            { BiomeType.LostRiverJunction_Wall,    (1, 1f) },
            { BiomeType.BloodKelp_TrenchFloor,     (1, 1f) },
            { BiomeType.BloodKelp_TrenchWall,      (1, 1f) },
            { BiomeType.BloodKelp_CaveFloor,       (1, 1f) },
            { BiomeType.BloodKelp_CaveWall,        (1, 1f) },
            { BiomeType.BloodKelp_Grass,           (1, 1f) },
            { BiomeType.BloodKelp_Floor,           (1, 1f) },
            { BiomeType.BloodKelp_Wall,            (1, 1f) },
        };



        public static Dictionary<BiomeType, (int, float)> RadiantCrystalBiomes = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.SafeShallows_Wall, (1, 1f) },
        };
    }
}