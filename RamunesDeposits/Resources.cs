using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramune.RamunesOutcrops
{
    public static class Resources
    {
        public static Dictionary<BiomeType, (int, float)> LodestoneOutcrop = new Dictionary<BiomeType, (int, float)>()   
        {
            { BiomeType.JellyshroomCaves_CaveFloor,   (1, 1f) },
            { BiomeType.JellyshroomCaves_CaveWall,    (1, 1f) },
            { BiomeType.JellyshroomCaves_CaveCeiling, (1, 1f) },
        };

        public static Dictionary<BiomeType, (int, float)> GeyseriteOutcrop = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.Dunes_ThermalVent,              (1, 1f) },
            { BiomeType.BonesField_ThermalVent,         (1, 1f) },
            { BiomeType.GrandReef_ThermalVent,          (1, 1f) },
            { BiomeType.DeepGrandReef_ThermalVent,      (1, 1f) },
            { BiomeType.LostRiverCorridor_ThermalVents, (1, 1f) },
            { BiomeType.LostRiverJunction_ThermalVent,  (1, 1f) },
            { BiomeType.Mountains_ThermalVent,          (1, 1f) },
        };

        public static Dictionary<BiomeType, (int, float)> SiltstoneOutcrop = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.MushroomForest_GiantTreeInteriorFloor, (1, 1f) },
            { BiomeType.MushroomForest_MushroomTreeTrunk,      (1, 1f) },
            { BiomeType.MushroomForest_CaveCeiling,            (1, 1f) },
            { BiomeType.MushroomForest_CaveFloor,              (1, 1f) },
            { BiomeType.MushroomForest_RockWall,               (1, 1f) },
            { BiomeType.MushroomForest_CaveWall,               (1, 1f) },
            { BiomeType.MushroomForest_Sand,                   (1, 1f) },
        };

        public static Dictionary<BiomeType, (int, float)> SerpentiniteOutcrop = new Dictionary<BiomeType, (int, float)>()
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

        public static Dictionary<BiomeType, (int, float)> RadiantCrystal = new Dictionary<BiomeType, (int, float)>()
        {
            { BiomeType.SafeShallows_Wall, (1, 1f) },
        };
    }
}