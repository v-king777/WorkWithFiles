using System;
using System.IO;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine("E:", "WorkWithFiles", "Task1", "TestFolder");
            Console.WriteLine(path);
            TimePathCleaner(path);
        }

        static void TimePathCleaner(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Указанного пути не существует");
                return;
            }

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(folderPath);

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
