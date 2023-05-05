using System;
using System.IO;
using System.Reflection;
using Nautilus.Utility;
using UnityEngine;

namespace RamuneLib.Utils
{
    public static class Texture
    {
        public static Texture2D Get(string filename) => ImageUtils.LoadTextureFromFile(IOUtilities.Combine(Assembly.GetExecutingAssembly().Location, "Assets", filename + ".png"));
    }
}