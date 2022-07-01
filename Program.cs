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
        private static string filter = "1";
        private static void Main(string[] args) {
            Console.WriteLine(title);
            for (var i = 0; i < args.Length; i++) {
                if (args[i] == "-p") {
                    path = args[i + 1];
                }

                if (args[i] == "-n") {
                    prepend = args[i + 1];
                }

                if (args[i] == "-f") {
                    filter = args[i + 1];
                }

                if (args[i] == "-h" || args[i] == "-help") {
                    Console.WriteLine(title);
                    Console.WriteLine("-p   :   path to files to be renamed.");
                    Console.WriteLine("-f   :   amount of filtering used in renaming files: 0 = removes only spaces, 1 = removes spaces and parentheses (DEFAULT), 2 = removes spaces and all special characters, 3 = removes all numbers.");
                    Console.WriteLine("-n   :   name to prepend to filename.  eg. '-n hi_ for hi_filename.txt.");
                    return;
                }
            }

            var isPathValid = validatePath();
            if(isPathValid) {
                Rename();
            }
        }

        private static void Rename() {
            var directoryInfo = new DirectoryInfo(@"" + path); 
            var files = directoryInfo.GetFiles();

            foreach (var info in files)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(info.Name);
                var newFilename = $"{prepend}{fileNameWithoutExtension}{info.Extension}";
                var cleanFileName = CleanFileName(newFilename, filter);
                var newFullFilename = Path.Combine(path, cleanFileName);

                File.Move(info.FullName, newFullFilename);

                Console.WriteLine("Renaming: {0} -> {1}", info.Name, newFullFilename);
            }
            Console.WriteLine("Total files renamed: {0}", files.Length);
        }

        private static bool validatePath() {
            if (string.IsNullOrEmpty(path)) {
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
                        var tryPathAgain = UtilsConsole.Confirm("Not a valid path, try again?");
                        if(tryPathAgain) {
                            path = "";
                            validatePath();
                        }
                        else {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private static string CleanFileName(string filename, string filterLevel) {
            Enum.TryParse(filterLevel, out FilterLevel filterLevelStatus);
            var cleanFilename = "";

            switch (filterLevelStatus) {
                case FilterLevel.SPACES:
                    cleanFilename = filename.Replace(" ", "");
                    break;

                case FilterLevel.SPECIAL_CHARACTERS:
                    Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                    cleanFilename = r.Replace(filename, String.Empty).Replace(" ", "");
                    break;

                case FilterLevel.NUMBERS:
                    cleanFilename = Regex.Replace(filename, @"[\d-]", string.Empty);
                    break;

                default:
                    cleanFilename = filename.Replace(" ", "").Replace("(", "").Replace(")", "");
                    break;
            }

            return cleanFilename;
        }
    }
}
