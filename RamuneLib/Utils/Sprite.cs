using System;
using System.IO;
using System.Reflection;
using Nautilus.Utility;

namespace RamuneLib.Utils
{
    public static class Sprite
    {
        public static Atlas.Sprite Get(object FileOrTechType)
        {
            if(FileOrTechType is TechType techType) return SpriteManager.Get(techType);
            else if(FileOrTechType is string filename) return ImageUtils.LoadSpriteFromFile(IOUtilities.Combine(Assembly.GetExecutingAssembly().Location, "Assets", filename + ".png"));
            else throw new ArgumentException("Incorrect type used in Sprite.Get()");
        }
    }
}