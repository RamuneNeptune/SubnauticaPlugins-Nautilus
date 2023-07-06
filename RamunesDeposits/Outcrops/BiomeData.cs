


using UnityEngine.UIElements;

namespace Ramune.RamunesOutcrops.Outcrops
{
    public static class BiomeData
    {
        public static List<SpawnLocation> RadiantCrystal = new()
        {
            new(new(868f,     -322.1f,  -1157f),     new(37.7f,    167.3f,  23.9f )),
            new(new(807.2f,   -285.3f,  -1202.66f),  new(-0.78f,   -5.3f,   5.3f  )),
            new(new(774.8f,   -311.1f,  -1161.2f),   new(338.47f,  0.91f,   -1.23f)),
            new(new(819.16f,  -282.6f,  -1149.3f),   new(-4.8f,    -2.6f,   -5.3f ))
        };

        public static Dictionary<BiomeType, (int, float)> Lodestone = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.JellyshroomCaves_CaveFloor,   (1, 0.3f) },
            { BiomeType.JellyshroomCaves_CaveWall,    (1, 0.2f) },
            { BiomeType.JellyshroomCaves_CaveCeiling, (1, 0.4f) },
        };

        public static Dictionary<BiomeType, (int, float)> Geyserite = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.Dunes_ThermalVent,              (1, 0.5f) },
            { BiomeType.Dunes_ThermalVent_Rock,         (1, 0.5f) },
            { BiomeType.Dunes_ThermalVent_Grass,        (1, 0.5f) },
            { BiomeType.BonesField_ThermalVent,         (1, 0.5f) },
            { BiomeType.GrandReef_ThermalVent,          (1, 0.5f) },
            { BiomeType.LostRiverCorridor_ThermalVents, (1, 0.5f) },
            { BiomeType.LostRiverJunction_ThermalVent,  (1, 0.5f) },
            { BiomeType.Mountains_ThermalVent,          (1, 0.5f) },
        };

        public static Dictionary<BiomeType, (int, float)> Siltstone = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.MushroomForest_GiantTreeInteriorFloor, (1, 0.5f) },
            { BiomeType.MushroomForest_MushroomTreeTrunk,      (1, 0.5f) },
            { BiomeType.MushroomForest_CaveCeiling,            (1, 0.5f) },
            { BiomeType.MushroomForest_CaveFloor,              (1, 0.5f) },
            { BiomeType.MushroomForest_RockWall,               (1, 0.5f) },
            { BiomeType.MushroomForest_CaveWall,               (1, 0.5f) },
            { BiomeType.MushroomForest_Sand,                   (1, 0.5f) },
        };

        public static Dictionary<BiomeType, (int, float)> Serpentite = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.LostRiverCorridor_Ceiling, (1, 0.5f) },
            { BiomeType.LostRiverCorridor_Wall,    (1, 0.5f) },
            { BiomeType.LostRiverJunction_Ceiling, (1, 0.5f) },
            { BiomeType.LostRiverJunction_Wall,    (1, 0.5f) },
            { BiomeType.BloodKelp_TrenchFloor,     (1, 0.5f) },
            { BiomeType.BloodKelp_TrenchWall,      (1, 0.5f) },
            { BiomeType.BloodKelp_CaveFloor,       (1, 0.5f) },
            { BiomeType.BloodKelp_CaveWall,        (1, 0.5f) },
            { BiomeType.BloodKelp_Grass,           (1, 0.5f) },
            { BiomeType.BloodKelp_Floor,           (1, 0.5f) },
            { BiomeType.BloodKelp_Wall,            (1, 0.5f) },
        };
    }
}