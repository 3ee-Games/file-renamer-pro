using System;
using System.IO;
namespace FileRenamer
{
    internal static class Program
    {
        private static void Main(string[] args) {
            var path = "";
            var prepend = "";
            for (var i = 0; i < args.Length; i++){
                if (args[i] == "-p") {
                    path = args[i + 1];
                }

                if (args[i] == "-n"){
                    prepend = args[i + 1];
                }

                if(args[i] == "-h") {
                    Console.WriteLine("Renamer CLI");
                    Console.WriteLine("-p   :   path to files to be renamed.");
                    Console.WriteLine("-n   :   name to prepend to filename.  eg. '-n hi_ for hi_filename.txt.");
                    return;
                }
            }

            if(string.IsNullOrEmpty(path)) {
                Console.WriteLine("No path provided.  Please provide -p {path}.  For example: c:\\myFolder\\files.  -h for help. ");
                return;
            }

            var directoryInfo = new DirectoryInfo(@"" + path);
            var files = directoryInfo.GetFiles();

            foreach (var info in files) {   
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(info.Name);
                var newFilename = $"{prepend}{fileNameWithoutExtension}.{info.Extension}";
                var cleanFileName = newFilename.Replace(" ", "").Replace("(", "").Replace(")", "");
                var newFullFilename = Path.Combine(path, cleanFileName);
                
                File.Move(info.FullName, newFullFilename);

                Console.WriteLine("Renaming: {0} -> {1}", info.FullName, newFullFilename);
            }
        }
    }
}
