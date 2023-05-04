
using System.IO;
using System.Reflection;
using Nautilus.Utility;
using FMOD;

namespace RamuneLib.Utils
{
    public static class Sound
    {
        public static FMOD.Sound Create(string filename) => AudioUtils.CreateSound(Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets"), filename + ".mp3"));
        public static void Play(FMOD.Sound sound, string bus) => AudioUtils.TryPlaySound(sound, bus, out _);
    }
}