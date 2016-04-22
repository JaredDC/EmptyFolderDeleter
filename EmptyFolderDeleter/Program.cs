using System;
using System.IO;
using System.Linq;

namespace EmptyFolderDeleter
{
    public class Program
    {
        private static string rootPath = @"C:\ReplaceWithDirectoryPath\";

        public static void Main(string[] args)
        {
            RecurseDirectory(rootPath);

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
