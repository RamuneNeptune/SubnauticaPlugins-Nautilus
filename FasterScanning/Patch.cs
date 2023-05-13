
using HarmonyLib;

namespace Ramune.FasterScanning
{
    [HarmonyPatch(typeof(PDAScanner), nameof(PDAScanner.Scan))]
    public static class PDAScannerPatch
    {
        public static float oldScanTime = 2f;
        public static float newScanTime;

        public static void Postfix()
        {
            newScanTime = oldScanTime / FasterScanning.config.ScanSpeed;

            if(FasterScanning.config.ScanSpeed > 4) newScanTime /= FasterScanning.config.ScanSpeed * 2;

            TechType techType = PDAScanner.scanTarget.techType;
            PDAScanner.EntryData entryData = PDAScanner.GetEntryData(techType);

            if(entryData == null) return;
            entryData.scanTime = newScanTime;
        }
    }
}