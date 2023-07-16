

namespace Ramune.RamunesOutcrops.Data
{
    public static class OutcropData
    {
        public static (TechType, TechType, float)[] Lodestone = new[]
        {
            (RamunesOutcrops.LodestoneOutcrop, TechType.Quartz, 0.3f),
            (RamunesOutcrops.LodestoneOutcrop, TechType.Lithium, 0.3f),
            (RamunesOutcrops.LodestoneOutcrop, TechType.Magnetite, 0.225f),
            (RamunesOutcrops.LodestoneOutcrop, TechType.Titanium, 0.3f)
        };

        public static (TechType, TechType, float)[] Geyserite = new[]     
        {
            (RamunesOutcrops.GeyseriteOutcrop, TechType.Lead, 0.2f),
            (RamunesOutcrops.GeyseriteOutcrop, TechType.Silver, 0.2f),
            (RamunesOutcrops.GeyseriteOutcrop, TechType.Gold, 0.15f)
        };
                
        public static (TechType, TechType, float)[] Siltstone = new[]
        {
            (RamunesOutcrops.SiltstoneOutcrop, TechType.Titanium, 0.2f),
            (RamunesOutcrops.SiltstoneOutcrop, TechType.Lithium, 0.2f),
            (RamunesOutcrops.SiltstoneOutcrop, TechType.Copper, 0.2f),
            (RamunesOutcrops.SiltstoneOutcrop, TechType.Silver, 0.2f)
        };

        public static (TechType, TechType, float)[] Serpentite = new[]
        {
            (RamunesOutcrops.SerpentiteOutcrop, TechType.Nickel, 0.1f),
            (RamunesOutcrops.SerpentiteOutcrop, TechType.Diamond, 0.2f),
            (RamunesOutcrops.SerpentiteOutcrop, TechType.AluminumOxide, 0.165f)
        };
    }
}
