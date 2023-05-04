using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Nautilus.Utility;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine;

namespace RamuneLib.Utils
{
    public static class Log
    {
        public static void Colored(string color, string text) => ErrorMessage.AddError(color + text + "</color>");
    }

    public static class Colors
    {
        public static string Red = "<color=#c2484f>";
        public static string Orange = "<color=#ff9706>";
        public static string Yellow = "<color=#f2cb5f>";
        public static string Green = "<color=#1eda62>";
        public static string Lime = "<color=#c4ff1f>";
        public static string Blue = "<color=#6bd6eb>";
        public static string Pink = "<color=#d76eff>";
        public static string Purple = "<color=#7f19f5>";
        public static string Grey = "<color=#a4a4a4>";
    }
}