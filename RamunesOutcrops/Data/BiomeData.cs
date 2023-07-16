

namespace Ramune.RamunesOutcrops.Data
{
    public static class BiomeData
    {
        public static Dictionary<BiomeType, (int, float)> Lodestone = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.JellyshroomCaves_CaveFloor,   (1, 0.09f) },
            { BiomeType.JellyshroomCaves_CaveWall,    (1, 0.09f) },
            { BiomeType.JellyshroomCaves_CaveCeiling, (1, 0.09f) },
        };

        public static Dictionary<BiomeType, (int, float)> Geyserite = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.Dunes_ThermalVent,              (1, 0.2f) },
            { BiomeType.Dunes_ThermalVent_Rock,         (1, 0.2f) },
            { BiomeType.Dunes_ThermalVent_Grass,        (1, 0.2f) },
            { BiomeType.BonesField_ThermalVent,         (1, 0.2f) },
            { BiomeType.GrandReef_ThermalVent,          (1, 0.2f) },
            { BiomeType.LostRiverCorridor_ThermalVents, (1, 0.2f) },
            { BiomeType.LostRiverJunction_ThermalVent,  (1, 0.2f) },
            { BiomeType.Mountains_ThermalVent,          (1, 0.2f) },
        };

        public static Dictionary<BiomeType, (int, float)> Siltstone = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.MushroomForest_GiantTreeInteriorFloor, (1, 0.09f) },
            { BiomeType.MushroomForest_MushroomTreeTrunk,      (1, 0.09f) },
            { BiomeType.MushroomForest_CaveCeiling,            (1, 0.09f) },
            { BiomeType.MushroomForest_CaveFloor,              (1, 0.09f) },
            { BiomeType.MushroomForest_RockWall,               (1, 0.09f) },
            { BiomeType.MushroomForest_CaveWall,               (1, 0.09f) },
            { BiomeType.MushroomForest_Sand,                   (1, 0.09f) },
        };

        public static Dictionary<BiomeType, (int, float)> Serpentite = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.LostRiverCorridor_Ceiling, (1, 0.125f) },
            { BiomeType.LostRiverCorridor_Wall,    (1, 0.125f) },
            { BiomeType.LostRiverJunction_Ceiling, (1, 0.125f) },
            { BiomeType.LostRiverJunction_Wall,    (1, 0.125f) },
            { BiomeType.BloodKelp_TrenchFloor,     (1, 0.125f) },
            { BiomeType.BloodKelp_TrenchWall,      (1, 0.125f) },
            { BiomeType.BloodKelp_CaveFloor,       (1, 0.125f) },
            { BiomeType.BloodKelp_CaveWall,        (1, 0.125f) },
            { BiomeType.BloodKelp_Grass,           (1, 0.125f) },
            { BiomeType.BloodKelp_Floor,           (1, 0.125f) },
            { BiomeType.BloodKelp_Wall,            (1, 0.125f) },
        };
    }
}