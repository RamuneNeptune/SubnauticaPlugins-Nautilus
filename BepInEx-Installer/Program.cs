
#pragma warning disable CS8601

using System.Text.RegularExpressions;
using System.Runtime.Versioning;
using static System.Console;
using Microsoft.Win32;
using System.Net;
using Octokit;

[SupportedOSPlatform("windows")]
public class Program
{
    public static RegistryKey Steam32 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\VALVE\\");
    public static RegistryKey Steam64 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Valve\\");
    public static Dictionary<string, string> debug = new Dictionary<string, string>
    {
        /*  GitHub: 1.   */ { " OK: Fetched zip file for BepInEx.Subnautica", " ERROR: Failed to match release name for BepInEx.Subnautica" },
        /*  GitHub: 2.   */ { " OK: Fetched zip file for Nautilus.Subnautica", " ERROR: Failed to match release name for Nautilus.Subnautica" },
        /*  GitHub: 3.   */ { " OK: Installed zip file for BepInEx.Subnautica", " ERROR: Failed to install zip file for BepInEx.Subnautica"},
        /*  GitHub: 4.   */ { " OK: Installed zip file for Nautilus.Subnautica", " ERROR: Failed to install zip file for Nautilus.Subnautica"},
        /*  Steam:  5.   */ { " OK: Found libraryfolders.vdf", " ERROR: Failed to find libraryfolders.vdf" },
        /*  Steam:  6.   */ { " OK: Found path in libraryfolders.vdf", " ERROR: Failed to find path in libraryfolders.vdf" },
        /*  Steam:  7.   */ { " OK: Found Subnautica directory", " ERROR: Failed to get Subnautica directory" }, 
        /*  Epic:   8.   */ { " OK: Found Epic Games manifests", " ERROR: Failed to find Epic Games manifests" },
        /*  Epic:   9.   */ { " OK: Found path in Epic Games manifests", "  ERROR: Failed to find path in Epic Games manifests" },
    };


    public static void Main()
    {
        WriteLine("\n   Select your platform:");
        Write("    (");
        ForegroundColor = ConsoleColor.Blue;
        Write("x");
        ForegroundColor = ConsoleColor.White;
        Write(") "); 
        ForegroundColor = ConsoleColor.Blue;
        Write("Steam");
        ForegroundColor = ConsoleColor.White;
        Write("  (");
        ForegroundColor = ConsoleColor.DarkYellow;
        Write("y");
        ForegroundColor = ConsoleColor.White;
        Write(") ");
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine("Epic\n");
        ForegroundColor = ConsoleColor.White;

        var key = ReadKey();
        if(key.Key == ConsoleKey.X) Begin(true);
        else if(key.Key == ConsoleKey.Y) Begin(false);
        else ThrowError(10);
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine("\n Press any key to exit");
        ForegroundColor = ConsoleColor.White;
        ReadKey();
    }


    public static async void Begin(bool isSteam)
    {
        Clear();
        var temp = Path.GetTempPath();
        var clientGit = new GitHubClient(new ProductHeaderValue("BepInEx-Installer"));
        var clientWeb = new WebClient();

        var SN_BepInExReleases = clientGit.Repository.Release.GetAll("toebeann", "BepInEx.Subnautica");
        var SN_BepInExLatest = SN_BepInExReleases.Result[0];
        var SN_BepInExZip = SN_BepInExLatest.Assets[1];
        if(SN_BepInExZip.Name.StartsWith("BepInEx_x64")) ThrowSuccess(1);
        else ThrowError(1);

        var NautilusReleases = clientGit.Repository.Release.GetAll("SubnauticaModding", "Nautilus");
        var NautilusLatest = NautilusReleases.Result[0];
        var SN_NautilusZip = NautilusLatest.Assets[1];
        if(SN_NautilusZip.Name.StartsWith("Nautilus_SN")) ThrowSuccess(2);
        else ThrowError(2);

        using(clientWeb)
        {
            if(!Uri.TryCreate(SN_BepInExZip.BrowserDownloadUrl, UriKind.Absolute, out var bepinex))
            {
                ThrowError(3);
                return;
            }
            else clientWeb.DownloadFileAsync(bepinex, Path.Combine(temp, SN_BepInExZip.Name));

            while(clientWeb.IsBusy) Thread.Sleep(100);

            ThrowSuccess(3);

            if(!Uri.TryCreate(SN_NautilusZip.BrowserDownloadUrl, UriKind.Absolute, out var nautilus))
            {
                ThrowError(4);
                return;
            }
            else clientWeb.DownloadFileAsync(nautilus, Path.Combine(temp, SN_NautilusZip.Name));

            while(clientWeb.IsBusy) Thread.Sleep(100);

            ThrowSuccess(4);

            File.Delete(Path.Combine(temp, SN_BepInExZip.Name));
            File.Delete(Path.Combine(temp, SN_NautilusZip.Name));
        }
        if(isSteam) FindSteam();
        else FindEpic();
    }


    public static string? FindSteam()
    {
        string libraryfolders = Steam64?.OpenSubKey("steam")?.GetValue("InstallPath")?.ToString() + @"\steamapps\libraryfolders.vdf";
        if(!File.Exists(libraryfolders))
        {
            ThrowError(5);
            return null;
        }
        else ThrowSuccess(5);

        string contents = File.ReadAllText(libraryfolders);
        string pattern = @"(""path""\s+""([^""]+)"")[^}]+?""264710""";

        var match = Regex.Match(contents, pattern);
        if(!match.Success)
        {
            ThrowError(6);
            return null;
        }
        else ThrowSuccess(6);

        string subnautica = match.Groups[1].Value.Replace("\"path\"", "").Replace(@"\\", @"\").Replace("\"", "").TrimStart() + @"\steamapps\common\Subnautica";
        if(!Directory.Exists(subnautica))
        {
            ThrowError(7);
            return null;
        }
        else ThrowSuccess(7);

        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"\n {subnautica}");
        ForegroundColor = ConsoleColor.White;
        return subnautica;
    }


    public static string? FindEpic()
    {
        string manifests = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Epic\EpicGamesLauncher\Data\Manifests");
        if(!Directory.Exists(manifests))
        {
            ThrowError(8); 
            return null;
        }
        else ThrowSuccess(8);

        string pattern = "\"InstallLocation\"[^\"]*\"(.*)\"";

        string[] files = Directory.GetFiles(manifests, "*.item");
        foreach(string file in files)
        {
            var match = Regex.Match(File.ReadAllText(file), pattern);
            if(!match.Success) break;
            if (match.Value.Contains("Subnautica") && !match.Value.Contains("Below") && Directory.Exists(match.Groups[1].Value.Replace("\\\\", "\\").Replace("/", "\\")))
            {
                ThrowSuccess(7);
                ThrowSuccess(9);
                ForegroundColor = ConsoleColor.DarkYellow;
                WriteLine($"\n {match.Groups[1].Value}");
                ForegroundColor = ConsoleColor.White;
                return match.Groups[1].Value;
            }
            else
            {
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLine($"{match.Groups[1].Value.Replace("\\\\", "\\").Replace("/", "\\")}");
                ForegroundColor = ConsoleColor.White;
            }
        }
        ThrowError(7);
        return null;
    }

    public static void ThrowError(int error)
    {
        ForegroundColor = ConsoleColor.Red;
        Write(debug.ElementAt(error - 1).Value.Split(":").GetValue(0) + ": ");
        ForegroundColor = ConsoleColor.White;
        WriteLine(debug.ElementAt(error - 1).Value.Split(":").GetValue(1));
    }

    public static void ThrowSuccess(int step)
    {
        ForegroundColor = ConsoleColor.Green;
        Write(debug.ElementAt(step -1).Key.Split(":").GetValue(0) + ": ");
        ForegroundColor = ConsoleColor.White;
        WriteLine(debug.ElementAt(step - 1).Key.Split(":").GetValue(1));
    }
}