using System;
using System.IO;
using System.Text.RegularExpressions;
namespace FileRenamerPro
{
    internal static class Program
    {
        public enum FilterLevel
        {
            Spaces = 0,
            SpacesParentheses = 1,
            SpecialCharacters = 2,
            Numbers = 3,
            Guid = 4
        }

        private const string Title = @"File Renamer PRO";
        private static string _path = string.Empty;
        private static string _prepend = string.Empty;
        private static string _append = string.Empty;
        public static FilterLevel _filter = FilterLevel.SpacesParentheses;
        private static bool _keepCase = false;

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Title);
            Console.ResetColor();

            if (HandleArguments(args) && ValidatePath())
            {
                RenameFiles();
            }
        }

        private static void RenameFiles()
        {
            var directoryInfo = new DirectoryInfo(_path);
            var files = directoryInfo.GetFiles();

            var renamedFilesCount = files.Count(file => TryRename(file));

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"* Total files renamed: {renamedFilesCount} out of {files.Length}");
            Console.WriteLine($"* Renamed using {_filter} filter");
            Console.ResetColor();
        }

        private static bool TryRename(FileInfo info)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(info.Name);
            var cleanFileName = CleanFileName(fileNameWithoutExtension);
            var newFilename = $"{_prepend}{cleanFileName}{_append}{info.Extension}";
            var newFullFilename = Path.Combine(_path, newFilename);

            if (info.FullName == newFullFilename)
            {
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Renaming: {info.Name} -> {newFilename}");
            File.Move(info.FullName, newFullFilename, true);

            return true;
        }

        private static bool ValidatePath()
        {
            if (string.IsNullOrEmpty(_path) || !Path.IsPathRooted(_path))
            {
                var pathToApplication = System.AppDomain.CurrentDomain.BaseDirectory;
                var useDefaultPathDisplay = $"No path provided.  Use local path: " + $"{pathToApplication} instead?";
                var useDefaultPath = UtilsConsole.Confirm(useDefaultPathDisplay);
                if (useDefaultPath)
                {
                    _path = pathToApplication;
                    return true;
                }
                else
                {
                    Console.WriteLine("Please enter path that contains files to be renamed:");
                    _path = Console.ReadLine();

                    if (string.IsNullOrEmpty(_path) || !Path.IsPathRooted(_path))
                    {
                        var errorMessage = $"{_path} is not a valid path, try again?";
                        var tryPathAgain = UtilsConsole.Confirm(errorMessage);
                        if (!tryPathAgain)
                        {
                            return false;
                        }
                        _path = "";
                        ValidatePath();
                    }
                }
            }
            return true;
        }

        public static string CleanFileName(string filename)
        {
            var cleanFilename = string.Empty;

            switch (_filter)
            {
                case FilterLevel.Spaces:
                    cleanFilename = _keepCase ? filename.Replace(" ", "") : filename.Replace(" ", "").ToLowerInvariant();
                    break;
                case FilterLevel.SpecialCharacters:
                    var regex = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                    cleanFilename = _keepCase ? regex.Replace(filename, string.Empty).Replace(" ", "") : regex.Replace(filename, string.Empty).Replace(" ", "").ToLowerInvariant();
                    break;
                case FilterLevel.Numbers:
                    cleanFilename = Regex.Replace(filename, @"[\d-]", string.Empty);
                    break;
                case FilterLevel.Guid:
                    cleanFilename = Guid.NewGuid().ToString();
                    break;
                case FilterLevel.SpacesParentheses:
                default:
                    cleanFilename = _keepCase ? filename.Replace(" ", "").Replace("(", "").Replace(")", "") : filename.Replace(" ", "").Replace("(", "").Replace(")", "").ToLowerInvariant();
                    break;
            }

            return cleanFilename;
        }


        private static bool HandleArguments(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-p":
                        _path = args[i + 1];
                        break;
                    case "-n":
                        _prepend = args[i + 1];
                        break;
                    case "-a":
                        _append = args[i + 1];
                        break;
                    case "-f":
                        _filter = (FilterLevel)Enum.Parse(typeof(FilterLevel), args[i + 1]);
                        break;
                    case "--keep-case":
                        _keepCase = true;
                        break;
                    case "-h":
                    case "-help":
                        PrintHelp();
                        return false;
                }
            }

            return true;
        }

        private static void PrintHelp()
        {
             Console.WriteLine("-p            :   path to files to be renamed.");
                    Console.WriteLine("-f            :   amount of filtering used in renaming files: 0 = removes only spaces, 1 = removes spaces and parentheses (DEFAULT), 2 = removes spaces and all special characters, 3 = removes all numbers.");
                    Console.WriteLine("-n            :   name to prepend to filename.  eg. '-n hi_ for hi_filename.txt.");
                    Console.WriteLine("-a            :   name to append to filename.  eg. '-a _bye for filename_bye.txt.");
                    Console.WriteLine("--keep-case   :   this flag will keep the case of the name of the file.");
        }
    }
}
