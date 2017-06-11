using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDiff
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstDirectoryPath, secondDirectoryPath;
            List<string> firstDirectoryFiles, secondDirectoryFiles;

            Console.Write("Enter the first directory: ");
            ReadDirectory(out firstDirectoryPath, out firstDirectoryFiles);

            Console.Write("Enter the second directory: ");
            ReadDirectory(out secondDirectoryPath, out secondDirectoryFiles);

            Console.WriteLine($"Comparing \"{firstDirectoryPath}\" to \"{secondDirectoryPath}\"");
            CompareDirectories(firstDirectoryPath, secondDirectoryPath, firstDirectoryFiles, secondDirectoryFiles);
            Console.WriteLine("Press any key to quit..");
            Console.ReadKey();
        }

        public static void ReadDirectory(out string path, out List<string> files)
        {
            files = new List<string>();
            do
            {
                path = Console.ReadLine();
                Console.WriteLine($"Attempting to acquire file list from \"{path}\".");

                if (Directory.Exists(path))
                {
                    files = GetFilesFromDirectory(path);
                }
                else
                {
                    Console.WriteLine($"\"{path}\" is not a valid directory.");
                    Console.WriteLine("Please enter another directory.");
                }
            } while (!Directory.Exists(path));
        }

        public static void CompareDirectories(string path1, string path2, List<string> dir1, List<string> dir2)
        {
            var tmpDir1 = new List<string>(dir1.Select(x => Path.GetFileName(x)));
            var tmpDir2 = new List<string>(dir2.Select(x => Path.GetFileName(x)));
            foreach(var file in dir1.Select(x => Path.GetFileName(x)))
            {
                if (tmpDir2.Contains(file))
                {
                    tmpDir1.Remove(file);
                    tmpDir2.Remove(file);
                }
            }
            if(tmpDir1.Count > 0)
            {
                Console.WriteLine($"Files only in {path1}:");
                foreach (var file in tmpDir1)
                {
                    Console.WriteLine(file);
                }
            }
            if (tmpDir2.Count > 0)
            {
                Console.WriteLine($"Files only in {path2}:");
                foreach (var file in tmpDir2)
                {
                    Console.WriteLine(file);
                }
            }
        }

        // Process all files in the directory passed in and prints file count
        public static List<string> GetFilesFromDirectory(string directory)
        {
            // Process the list of files found in the directory.
            var files = Directory.GetFiles(directory).ToList();
            Console.WriteLine($"{files.Count} files found in \"{directory}\"");
            return files;
        }

    }
}
