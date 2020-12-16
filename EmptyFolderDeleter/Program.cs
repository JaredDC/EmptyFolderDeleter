using System;
using System.IO;
using System.Linq;

namespace EmptyFolderDeleter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var startingPath = string.Empty;

            if (args.Length == 0)
            {
                Console.WriteLine("Drag a folder onto me or manually enter a directory path:");
                startingPath = Console.ReadLine();
            }
            else
            {
                startingPath = args[0];
            }

            if (Directory.Exists(startingPath))
            {
                Console.WriteLine("The following folders will be deleted:");
                RecurseDirectoryPrint(startingPath);

                if (i != 1)
                {
                    Console.WriteLine("*********************************************");
                    Console.WriteLine("Do you want to continue?(y/n)");
                    int s = Console.Read();
                    if ('y' == s)
                    {
                        RecurseDirectory(startingPath);
                        Console.WriteLine();
                        Console.WriteLine("Successfully delete all empty folders recursively.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("You have canceled the delete operation.");
                    }
                }
                else {
                    Console.WriteLine("[NOTHING]");
                    Console.WriteLine("The folder is clean.");
                }
            }
            else
            {
                Console.WriteLine("Invalid directory path.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static int i = 1;
        private static void RecurseDirectory(string path)
        {
            try
            {
                var folders = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

                foreach (var folder in folders)
                {
                    RecurseDirectory(folder);
                }

                var results = Directory.GetDirectories(path).Any() || Directory.GetFiles(path).Any();

                if (!results && GetDirectorySize(path) == 0)
                {
                    Directory.Delete(path, false);

                    Console.WriteLine("Deleted: {0}", path);
                }
            }
            catch (Exception)
            {
            }
        }
        private static void RecurseDirectoryPrint(string path)
        {          
            try
            {
                var folders = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

                foreach (var folder in folders)
                {
                    RecurseDirectoryPrint(folder);
                }

                var results = Directory.GetDirectories(path).Any() || Directory.GetFiles(path).Any();

                if (!results && GetDirectorySize(path) == 0)
                {
                    Console.WriteLine("[{0}]: {1}", i, path);
                    i++;
                }
            }
            catch (Exception)
            {
            }
         }
        private static long GetDirectorySize(string path)
        {
            var fileNames = Directory.GetFiles(path, "*.*");
            long size = 0;

            foreach (var fileName in fileNames)
            {
                var info = new FileInfo(fileName);

                size += info.Length;
            }

            return size;
        }
    }
}
