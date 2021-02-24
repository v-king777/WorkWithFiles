using System;
using System.IO;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            TimePathCleaner(@"D:\WorkWithFiles\Task1\TestFolder\");
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

                foreach (var item in dirInfo.GetDirectories())
                {
                    if (DateTime.Now - item.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        item.Delete(true);
                        Console.WriteLine("Папка '{0}' удалена", item.Name);
                    }
                }

                foreach (var item in dirInfo.GetFiles())
                {
                    if (DateTime.Now - item.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        item.Delete();
                        Console.WriteLine("Файл '{0}' удалён", item.Name);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
        }
    }
}
