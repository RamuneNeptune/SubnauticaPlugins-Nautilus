

using Nautilus.Crafting;
using Nautilus.Handlers;
using static CraftData;
using System.Collections.Generic;
using System;
using UnityEngine;
using UWE;
using System.IO;
using System.Collections;

namespace RamuneLib
{
    public static class Main
    {
        public static string HelloThereDecompiler = @"
        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                         ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣶⣿⣿⣿⣿⣿⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                         ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⣿⣿⠿⠟⠛⠻⣿⠆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                         ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣆⣀⣀⠀⣿⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                         ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠻⣿⣿⣿⠅⠛⠋⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                         ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢼⣿⣿⣿⣃⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣟⡿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣛⣛⣫⡄⠀⢸⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⣴⣾⡆⠸⣿⣿⣿⡷⠂⠨⣿⣿⣿⣿⣶⣦⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣤⣾⣿⣿⣿⣿⡇⢀⣿⡿⠋⠁⢀⡶⠪⣉⢸⣿⣿⣿⣿⣿⣇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⣿⣿⣿⣿⣿⣿⣿⡏⢸⣿⣷⣿⣿⣷⣦⡙⣿⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⣿⣿⣿⣿⣿⣿⣿⣇⢸⣿⣿⣿⣿⣿⣷⣦⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣵⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣯⡁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                      We're no strangers to love
                                    You know the rules and so do I
                                A full commitment's what I'm thinking of
                                You wouldn't get this from any other guy

                                 I just wanna tell you how I'm feeling
                                     Gotta make you understand

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you

                                  We've known each other for so long
                                    Your heart's been aching, but
                                       You're too shy to say it
                                Inside, we both know what's been going on
                                 We know the game and we're gonna play it

                                   And if you ask me how I'm feeling
                                 Don't tell me you're too blind to see

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you

                                        (Ooh, give you up)
                                        (Ooh, give you up)
                                Never gonna give, never gonna give
                                          (Give you up)
                                Never gonna give, never gonna give
                                          (Give you up)

                                 We've known each other for so long
                                   Your heart's been aching, but
                                     You're too shy to say it
                              Inside, we both know what's been going on
                               We know the game and we're gonna play it

                                I just wanna tell you how I'm feeling
                                     Gotta make you understand

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you";


        public static readonly HashSet<string> PiracyFiles = new HashSet<string> { "steam_api64.cdx", "steam_api64.ini", "steam_emu.ini", "valve.ini", "chuj.cdx", "SteamUserID.cfg", "Achievements.bin", "steam_settings", "user_steam_id.txt", "account_name.txt", "ScreamAPI.dll", "ScreamAPI32.dll", "ScreamAPI64.dll", "SmokeAPI.dll", "SmokeAPI32.dll", "SmokeAPI64.dll", "Free Steam Games Pre-installed for PC.url", "Torrent-Igruha.Org.URL", "oalinst.exe", };

        public static void FindPiracy()
        {
            foreach (var file in PiracyFiles)
            {
                if (File.Exists(Path.Combine(Environment.CurrentDirectory, file)))
                {
                    CoroutineHost.StartCoroutine(LogError());
                    break;
                }
            }
        }

        public static IEnumerator LogError()
        {
            yield return new WaitForSeconds(1);
            Console.WriteLine("Purchase the game at discounted prices here: https://isthereanydeal.com/game/subnautica/info/");
            ErrorMessage.AddError("Purchase the game at discounted prices here: https://isthereanydeal.com/game/subnautica/info/");
        }
    }
}
