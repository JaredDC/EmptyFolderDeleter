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
                Console.WriteLine("Enter directory path to clean:");
                startingPath = Console.ReadLine();
            }
            else
            {
                startingPath = args[0];
            }

            if (Directory.Exists(startingPath))
            {
                RecurseDirectory(startingPath);
            }
            else
            {
                Console.WriteLine("Invalid directory path.");
            }

            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadKey();
        }

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
