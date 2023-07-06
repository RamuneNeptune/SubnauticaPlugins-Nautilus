using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramune.RadiantResources.Data
{
    public static class BiomeData
    {
        public static Dictionary<BiomeType, (int, float)> RadiantCrystal = new()
        {
            { BiomeType.JellyshroomCaves_CaveFloor,   (1, 0.3f) },
            { BiomeType.JellyshroomCaves_CaveWall,    (1, 0.2f) },
            { BiomeType.JellyshroomCaves_CaveCeiling, (1, 0.4f) },
        };

        public static Dictionary<BiomeType, (int, float)> RadiantOre = new()
        {
            { BiomeType.JellyshroomCaves_CaveFloor,   (1, 0.3f) },
            { BiomeType.JellyshroomCaves_CaveWall,    (1, 0.2f) },
            { BiomeType.JellyshroomCaves_CaveCeiling, (1, 0.4f) },
        };
    }
}