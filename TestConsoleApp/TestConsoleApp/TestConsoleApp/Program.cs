using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
              Console.Write("Input first Path directory:");
              string FirstDirPath = Console.ReadLine();

              Console.Write("Input first Path directory:");
              string SecondDirPath = Console.ReadLine();

              Console.Write("Input path for file:");
              string FileDirPath = Console.ReadLine();

              DirectoryInfo directoryInfoFirst = new DirectoryInfo(@FirstDirPath);
              DirectoryInfo directoryInfoSecond = new DirectoryInfo(@SecondDirPath);
              DirectoryInfo directoryInfo = new DirectoryInfo(@FileDirPath);

              if (directoryInfoFirst.Exists && directoryInfoSecond.Exists && directoryInfo.Exists)
              {
                    FileInfo[] AllFilesDirFirst = directoryInfoFirst.GetFiles("*.*", SearchOption.AllDirectories);
                    FileInfo[] AllFilesDirSecond = directoryInfoSecond.GetFiles("*.*", SearchOption.AllDirectories);

                    FileStream fileStream = File.Open(@FileDirPath + "/result.txt", FileMode.Create);

                    StreamWriter output = new StreamWriter(fileStream); 

                    foreach (var d in AllFilesDirFirst.Except(AllFilesDirSecond, new FileSystemInfoComparer()))
                        output.WriteLine(d);
                    output.Close();

                    Console.Write("Successfully completed!!");
                    Console.ReadKey();  
              }
              else {
                    Console.WriteLine("Error in path!!");
              }
        }
    }

    class FileSystemInfoComparer : IEqualityComparer<FileSystemInfo>
    {

        public bool Equals(FileSystemInfo f1, FileSystemInfo f2)
        {
            FileStream file1 = File.Open(f1.FullName, FileMode.OpenOrCreate);
            FileStream file2 = File.Open(f2.FullName, FileMode.OpenOrCreate);
            return (f1.Name == f2.Name) && (file1.Length == file2.Length);
        }

        public int GetHashCode(FileSystemInfo fsi)
        {
            return fsi.Name.GetHashCode();
        }
    }
}
