using System;
using System.IO;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = Path.Combine("E:", "WorkWithFiles", "Task2", "TestFolder");
            double folderSize = 0;

            Console.WriteLine(folderPath);

            FolderSizeCalculation(folderPath, ref folderSize);

            if (folderSize != 0)
            {
                Console.WriteLine("Размер папки: {0} байт", folderSize);
            }
        }

        static void FolderSizeCalculation(string path, ref double size)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Указанного пути не существует");
                return;
            }

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);

                foreach (var file in dirInfo.GetFiles())
                {
                    size += file.Length;
                }

                foreach (var dir in dirInfo.GetDirectories())
                {
                    FolderSizeCalculation(dir.FullName, ref size);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла ошибка: " + e.Message);
            }
        }
    }
}
