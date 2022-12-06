using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.models.enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.services
{
    internal class LessonService
    {
        public void SaveLessons()
        {
            using (StreamWriter file = new StreamWriter(@"../../../resources/lessons.txt"))
            {
                foreach (Lesson lesson in Util.Instance.Lessons)
                {
                    file.WriteLine(lesson.FormatTxtFileLine());
                }
            }
        }

        public void ReadLessons()
        {
            Util.Instance.Lessons = new ObservableCollection<Lesson>();
            using StreamReader file = new StreamReader(@"../../../resources/lessons.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {

                //1;1;03-10-2022;18:43;1:45;FREE;1;True
                string[] lajs = line.Split(';');
                Professor professor = Util.Instance.Professors.FirstOrDefault(c => c.ID == int.Parse(lajs[1]));
                Student student = Util.Instance.Students.FirstOrDefault(c => c.ID == int.Parse(lajs[6]));

                Util.Instance.Lessons.Add(new Lesson
                {
                    ID = int.Parse(lajs[0]),
                    Professor = professor,
                    Date = lajs[2],
                    StartTime = lajs[3],
                    Duration = lajs[4],
                    Status = (ELessonStatus)Enum.Parse(typeof(ELessonStatus), lajs[5]),
                    Student = student,
                    Active = bool.Parse(lajs[7])
                });

            }
            file.Close();
        }
    }
}
