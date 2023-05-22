using System;
using System.IO;
using System.Text.RegularExpressions;
namespace FileRenamerPro {
    internal static class Program {
        public enum FilterLevel { 
            SPACES = 0, 
            SPACES_PARENTHESES = 1, 
            SPECIAL_CHARACTERS = 2, 
            NUMBERS = 3,
            GUID = 4
        };
        private static string title = @"
  _____.__.__                                                                                    
_/ ____\__|  |   ____   _______   ____   ____ _____    _____   ___________  _____________  ____  
\   __\|  |  | _/ __ \  \_  __ \_/ __ \ /    \\__  \  /     \_/ __ \_  __ \ \____ \_  __ \/  _ \ 
 |  |  |  |  |_\  ___/   |  | \/\  ___/|   |  \/ __ \|  Y Y  \  ___/|  | \/ |  |_> >  | \(  <_> )
 |__|  |__|____/\___  >  |__|    \___  >___|  (____  /__|_|  /\___  >__|    |   __/|__|   \____/ 
                    \/               \/     \/     \/      \/     \/        |__|                 ";
        private static string path = "";
        private static string prepend = "";
        private static string append = "";
        private static string filter = "1";
        private static bool keepCase = false;
        private static void Main(string[] args) {

            PaintTitleScreen();
            ArgumentHandler(args);

            var isPathValid = validatePath();
            if(isPathValid) {
                Rename();
            }
        }

        private static void Rename() {
            var directoryInfo = new DirectoryInfo(@"" + path); 
            var files = directoryInfo.GetFiles();
            var fileCounter = 0;

            foreach (var info in files) {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(info.Name);
                var cleanFileName = CleanFileName(fileNameWithoutExtension, filter);
                var newFilename = $"{prepend}{cleanFileName}{append}{info.Extension}";
                var newFullFilename = Path.Combine(path, newFilename);

                if(info.FullName != newFullFilename) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Renaming: {0} -> {1}", info.Name, newFilename);
                    File.Move(info.FullName, newFullFilename, true);
                    fileCounter++;
                }
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("* Total files renamed: {0} out of {1}", fileCounter, files.Length);
            Enum.TryParse(filter, out FilterLevel filterLevelStatus);
            Console.WriteLine("* Renamed using {0} filter", filterLevelStatus.ToString());
            Console.ResetColor();
        }

        private static bool validatePath() {
            if (string.IsNullOrEmpty(path) || !Path.IsPathRooted(path)) {
                var pathToApplication = System.AppDomain.CurrentDomain.BaseDirectory;
                var useDefaultPathDisplay = $"No path provided.  Use local path: " + $"{pathToApplication} instead?";
                var useDefaultPath = UtilsConsole.Confirm(useDefaultPathDisplay);
                if (useDefaultPath) {
                    path = pathToApplication;
                    return true;
                } 
                else {
                    Console.WriteLine("Please enter path that contains files to be renamed:");
                    path = Console.ReadLine();

                    if(string.IsNullOrEmpty(path) || !Path.IsPathRooted(path)){
                        var errorMessage = $"{path} is not a valid path, try again?";
                        var tryPathAgain = UtilsConsole.Confirm(errorMessage);
                        if(!tryPathAgain) {
                            return false;
                        }
                        path = "";
                        validatePath();
                    }
                }
            }
            return true;
        }

        public static string CleanFileName(string filename, string filterLevel) {
            Enum.TryParse(filterLevel, out FilterLevel filterLevelStatus);
            var cleanFilename = "";

            switch (filterLevelStatus) {
                case FilterLevel.SPACES:
                    cleanFilename = (keepCase) ? filename.Replace(" ", "") : filename.Replace(" ", "").ToLowerInvariant();
                    break;

                case FilterLevel.SPECIAL_CHARACTERS:
                    Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                    cleanFilename = (keepCase) ? r.Replace(filename, String.Empty).Replace(" ", "") : r.Replace(filename, String.Empty).Replace(" ", "").ToLowerInvariant();
                    break;

                case FilterLevel.NUMBERS:
                    cleanFilename = Regex.Replace(filename, @"[\d-]", string.Empty);
                    break;

                case FilterLevel.GUID:
                    cleanFilename =  Guid.NewGuid().ToString();
                    break;

                case FilterLevel.SPACES_PARENTHESES:
                default:
                    cleanFilename = (keepCase) ? filename.Replace(" ", "").Replace("(", "").Replace(")", "") : filename.Replace(" ", "").Replace("(", "").Replace(")", "").ToLowerInvariant();
                    break;
            }

            return cleanFilename;
        }

        private static void PaintTitleScreen() {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        private static void ArgumentHandler(string[] args) {
            for (var i = 0; i < args.Length; i++) {
                if (args[i] == "-p") {
                    path = args[i + 1];
                }

                if (args[i] == "-n") {
                    prepend = args[i + 1];
                }

                if (args[i] == "-a") {
                    append = args[i + 1];
                }

                if (args[i] == "-f") {
                    filter = args[i + 1];
                }

                if(args[i] == "--keep-case") {
                    keepCase = true;
                }

                if (args[i] == "-h" || args[i] == "-help") {
                    Console.WriteLine("-p            :   path to files to be renamed.");
                    Console.WriteLine("-f            :   amount of filtering used in renaming files: 0 = removes only spaces, 1 = removes spaces and parentheses (DEFAULT), 2 = removes spaces and all special characters, 3 = removes all numbers.");
                    Console.WriteLine("-n            :   name to prepend to filename.  eg. '-n hi_ for hi_filename.txt.");
                    Console.WriteLine("-a            :   name to append to filename.  eg. '-a _bye for filename_bye.txt.");
                    Console.WriteLine("--keep-case   :   this flag will keep the case of the name of the file.");
                    return;
                }
            }
        }
    }
}
