using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var desktop = Environment.SpecialFolder.Desktop;
            var dirPath = Path.Combine(Environment.GetFolderPath(desktop), "Students");
            var filePath = Path.Combine(Environment.GetFolderPath(desktop), "Students.dat");

            if (!Directory.Exists(dirPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
                dirInfo.Create();
                Console.WriteLine("Папка 'Students' создана на рабочем столе");
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл 'Students.dat' не найден, поместите его на рабочий стол");
                return;
            }

            ReadFile(filePath);
        }

        /// <summary>
        /// Метод десериализует данные из файла Students.dat
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Возвращает массив объектов класса Student</returns>
        static Student[] ReadFile(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (var fs = new FileStream(path, FileMode.Open))
            {
                var newStudent = (Student[])formatter.Deserialize(fs);

                foreach (var item in newStudent)
                {
                    Console.WriteLine("Студент {0}, группа {1}, дата рождения {2}", item.Name, item.Group, item.DateOfBirth);
                }

                return newStudent;
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
