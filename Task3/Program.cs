using System;
using System.IO;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = Path.Combine("E:", "WorkWithFiles", "Task3", "TestFolder");
            double folderSize = 0;

            Console.WriteLine(folderPath);

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Указанного пути не существует");
                return;
            }

            FolderSizeCalculation(folderPath, ref folderSize);

            if (folderSize == 0)
            {
                Console.WriteLine("Папка пуста");
                return;
            }

            var OldFolderSize = folderSize;

            Console.WriteLine("Исходный размер папки: {0} байт", OldFolderSize);

            TimePathCleaner(folderPath);

            folderSize = 0;

            FolderSizeCalculation(folderPath, ref folderSize);

            var NewFolderSize = folderSize;

            Console.WriteLine("Очищено {0} байт", OldFolderSize - NewFolderSize);
            Console.WriteLine("Текущий размер папки: {0} байт", NewFolderSize);
        }

        static void FolderSizeCalculation(string path, ref double size)
        {
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

        static void TimePathCleaner(string path)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);

                foreach (var dir in dirInfo.GetDirectories())
                {
                    if (DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        dir.Delete(true);
                        Console.WriteLine("Папка '{0}' удалена", dir.Name);
                    }
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    if (DateTime.Now - file.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        file.Delete();
                        Console.WriteLine("Файл '{0}' удалён", file.Name);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла ошибка: " + e.Message);
            }
        }
    }
}
