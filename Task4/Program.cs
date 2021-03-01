using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program
    {
        static readonly Environment.SpecialFolder Desktop = Environment.SpecialFolder.Desktop;
        static readonly string DirPath = Path.Combine(Environment.GetFolderPath(Desktop), "Students");
        static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Desktop), "Students.dat");

        static void Main(string[] args)
        {
            try
            {
                CreateDirectory();
                SortByGroup();
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException)
            {
                Console.WriteLine("Отказано в доступе: " + ex.Message);
            }
            catch (Exception ex) when (ex is FileNotFoundException)
            {
                Console.WriteLine("Файл не найден: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }

            Console.WriteLine("\nДля продолжения нажмите любую клавишу . . .");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Метод создаёт папку на рабочем столе
        /// </summary>
        static void CreateDirectory()
        {
            if (Directory.Exists(DirPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(DirPath);
                dirInfo.Delete(true);
            }

            Directory.CreateDirectory(DirPath);
            Console.WriteLine("Папка 'Students' создана на рабочем столе\n");
        }

        /// <summary>
        /// Метод десериализует данные из файла Students.dat
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Массив объектов класса Student</returns>
        static Student[] ReadFile(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var newStudent = (Student[])formatter.Deserialize(fs);
                Console.WriteLine("Получены данные из файла 'Students.dat'\n");
                return newStudent;
            }
        }

        /// <summary>
        /// Метод раскидывает студентов по группам и пишет в отдельные текстовые файлы
        /// </summary>
        static void SortByGroup()
        {
            var students = ReadFile(FilePath);
            foreach (var item in students)
            {
                string path = Path.Combine(DirPath, item.Group + ".txt");
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        foreach (var item2 in students)
                        {
                            if (item2.Group == item.Group)
                            {
                                sw.WriteLine("Студент {0}, дата рождения {1}", item2.Name, item2.DateOfBirth);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Студенты раскиданы по группам в отдельные файлы:\n");
            string[] files = Directory.GetFiles(DirPath);
            foreach (var item in files)
            {
                Console.WriteLine(item);
            }
        }
    }

    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }
    }
}
