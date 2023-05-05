
using HarmonyLib;

namespace Ramune.FasterScanning
{
    [HarmonyPatch(typeof(PDAScanner), nameof(PDAScanner.Scan))]
    public static class PDAScannerPatch
    {
        public static void Postfix()
        {
            float oldScanTime = 2f;
            float newScanTime = oldScanTime / FasterScanning.config.ScanSpeed;

            if (FasterScanning.config.ScanSpeed > 4)
            {
                newScanTime /= FasterScanning.config.ScanSpeed * 2;
            }

            TechType techType = PDAScanner.scanTarget.techType;
            PDAScanner.EntryData entryData = PDAScanner.GetEntryData(techType);

            if(entryData != null)
            {
                entryData.scanTime = newScanTime;
                ErrorMessage.AddError($"New: {newScanTime}");
            }
            else PDAScanner.scanTarget.progress = PDAScanner.scanTarget.progress * FasterScanning.config.ScanSpeed;

            ErrorMessage.AddError($"<color=#f8cb4f>{PDAScanner.scanTarget.progress}</color>");
        }
    }
}