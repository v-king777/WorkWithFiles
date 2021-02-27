using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            var desktop = Environment.SpecialFolder.Desktop;
            var dirPath = Path.Combine(Environment.GetFolderPath(desktop), "Students");

            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

            if (!Directory.Exists(dirPath))
            {
                dirInfo.Create();
            }

            string filePath = Path.Combine(Environment.GetFolderPath(desktop), "Students.dat");

            if (File.Exists(filePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    var newStudent = (Student)formatter.Deserialize(fs);
                    Console.WriteLine("Объект десериализован");
                    Console.WriteLine("Имя: " + newStudent.Name);
                    Console.WriteLine("Группа: " + newStudent.Group);
                    Console.WriteLine("Дата рождения: " + newStudent.DateOfBirth);
                }
            }
        }
    }

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
