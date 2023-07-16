

using System.Net;
using Octokit;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO.Compression;

namespace BepInEx.Installer
{
    public static class Program
    {
        public static RegistryKey Steam64 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Valve\\");

        public static void Main()
        {
            var snPath = PathToSubnautica();
            Write($" Found Subnautica installation at: ");
            WriteLine($"{snPath}", ConsoleColor.DarkYellow);

            if(!Directory.Exists(snPath + "\\BepInEx")) TryDownload(snPath, "toebeann", "BepInEx.Subnautica");
            else WriteLine(" - Found BepInEx installation, skipping BepInEx..", ConsoleColor.Yellow);

            if(!Directory.Exists(snPath + "\\BepInEx\\plugins\\Modding Helper")) TryDownload(snPath, "SubnauticaModding", "Nautilus");
            else WriteLine(" - Found SMLHelper installation, skipping SMLHelper..", ConsoleColor.Yellow);

            Write("", ConsoleColor.White, true);
        }

        public static void TryDownload(string path, string user, string repo)
        {
            var clientGit = new GitHubClient(new ProductHeaderValue("BepInEx.Installer"));
            var clientWeb = new WebClient();
            var temp = Path.GetTempPath();

            var latest = clientGit.Repository.Release.GetLatest(user, repo);
            var zip = latest.Result.Assets.First();

            if(repo == "Nautilus") Write($"\n - Found zip file for SMLHelper: ");
            else Write($"\n - Found zip file for {repo}: ");
            WriteLine(zip.Name, ConsoleColor.DarkYellow);

            using (clientWeb)
            {
                if (!Uri.TryCreate(zip.BrowserDownloadUrl, UriKind.Absolute, out var file)) return;
                else clientWeb.DownloadFileAsync(file, Path.Combine(temp, zip.Name));
                while (clientWeb.IsBusy) Thread.Sleep(100);
            }

            if(zip.Name == "BepInEx.zip") ZipFile.ExtractToDirectory(Path.Combine(temp, zip.Name), path);
            else ZipFile.ExtractToDirectory(Path.Combine(temp, zip.Name), path + "\\BepInEx");

            File.Delete(Path.Combine(path, zip.Name));

            WriteLine(" - Installed successfully", ConsoleColor.Green, true);
        }

        public static string? PathToSubnautica()
        {
            string pattern = @"(""path""\s+""([^""]+)"")[^}]+?""264710""";

            string libraryfolders = Steam64?.OpenSubKey("steam")?.GetValue("InstallPath")?.ToString() + @"\steamapps\libraryfolders.vdf";
            if (!File.Exists(libraryfolders))
            {
                return null;
            }

            string contents = File.ReadAllText(libraryfolders);

            var match = Regex.Match(contents, pattern);
            if (!match.Success)
            {
                return null;
            }

            string subnautica = match.Groups[1].Value.Replace("\"path\"", "").Replace(@"\\", @"\").Replace("\"", "").TrimStart() + @"\steamapps\common\Subnautica";
            if (!Directory.Exists(subnautica))
            {
                return null;
            }

            return subnautica;
        }

        public static void WriteLine(string text, ConsoleColor color = ConsoleColor.White, bool shouldRead = false) 
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{text}");
            if(!shouldRead) return;
            Console.Read();
        }

        public static void Write(string text, ConsoleColor color = ConsoleColor.White, bool shouldRead = false)
        {
            Console.ForegroundColor = color;
            Console.Write($"{text}");
            if(!shouldRead) return;
            Console.Read();
        }
    }
}