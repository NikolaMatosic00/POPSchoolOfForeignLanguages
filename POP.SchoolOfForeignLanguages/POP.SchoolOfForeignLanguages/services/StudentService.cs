using POP.SchoolOfForeignLanguages.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.services
{
    internal class StudentService
    {
        public void SaveStudents()
        {
            using (StreamWriter file = new StreamWriter(@"../../../resources/students.txt"))
            {
                foreach (Student student in Util.Instance.Students)
                {
                    file.WriteLine(student.FormatTxtFileLine());
                }
            }
        }

        public void ReadStudents()
        {
            Util.Instance.Students = new ObservableCollection<Student>();
            using StreamReader file = new StreamReader(@"../../../resources/students.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] lajs = line.Split(';');
                RegisteredUser user = Util.Instance.Users.FirstOrDefault(c => c.ID == int.Parse(lajs[1]));
                Util.Instance.Students.Add(new Student
                {
                    ID = int.Parse(lajs[0]),
                    User = user,
                    Lessons = new List<Lesson>(),
                    Active = bool.Parse(lajs[2])
                });

                Console.WriteLine(line);
            }
            file.Close();
        }
    }
}
