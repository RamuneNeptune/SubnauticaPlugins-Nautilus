
using BepInEx.Logging;
using BepInEx;
using RamuneLib;
using System;

namespace Ramune.DecoFabricator
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class DecoFabricator : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private const string myGUID = "com.ramune.DecoFabricator";
        private const string pluginName = "Deco Fabricator";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            Main.FindPiracy();
            Fabricator.Patch();
            Console.WriteLine($"Loaded [{pluginName} {versionString}]");
            logger = Logger;
        }
    }
}