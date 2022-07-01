using System;
using System.IO;
namespace FileRenamerPro
{
    internal static class Program
    {
        private static void Main(string[] args) {
            string title = @"
  _____.__.__                                                                                    
_/ ____\__|  |   ____   _______   ____   ____ _____    _____   ___________  _____________  ____  
\   __\|  |  | _/ __ \  \_  __ \_/ __ \ /    \\__  \  /     \_/ __ \_  __ \ \____ \_  __ \/  _ \ 
 |  |  |  |  |_\  ___/   |  | \/\  ___/|   |  \/ __ \|  Y Y  \  ___/|  | \/ |  |_> >  | \(  <_> )
 |__|  |__|____/\___  >  |__|    \___  >___|  (____  /__|_|  /\___  >__|    |   __/|__|   \____/ 
                    \/               \/     \/     \/      \/     \/        |__|                 ";
            var path = "";
            var prepend = "";
            for (var i = 0; i < args.Length; i++){
                if (args[i] == "-p") {
                    path = args[i + 1];
                }

                if (args[i] == "-n"){
                    prepend = args[i + 1];
                }

                if(args[i] == "-h" || args[i] == "-help") {
                    Console.WriteLine(title);
                    Console.WriteLine("-p   :   path to files to be renamed.");
                    Console.WriteLine("-n   :   name to prepend to filename.  eg. '-n hi_ for hi_filename.txt.");
                    return;
                }
            }

            if(string.IsNullOrEmpty(path)) {
                Console.WriteLine(title);
                Console.WriteLine("No path provided.  Please provide -p {path}.  For example: c:\\myFolder\\files.  -h for help. ");
                return;
            }

            var directoryInfo = new DirectoryInfo(@"" + path);
            var files = directoryInfo.GetFiles();
            Console.WriteLine(title);

            foreach (var info in files) {   
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(info.Name);
                var newFilename = $"{prepend}{fileNameWithoutExtension}{info.Extension}";
                var cleanFileName = newFilename.Replace(" ", "").Replace("(", "").Replace(")", "");
                var newFullFilename = Path.Combine(path, cleanFileName);

                File.Move(info.FullName, newFullFilename);

                Console.WriteLine("Renaming: {0} -> {1}", info.FullName, newFullFilename);
            }
        }
    }
}
