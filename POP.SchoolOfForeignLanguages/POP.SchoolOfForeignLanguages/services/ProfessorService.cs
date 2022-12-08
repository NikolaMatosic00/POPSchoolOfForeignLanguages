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
    internal class ProfessorService
    {
        public void SaveProfessors()
        {
            using (StreamWriter file = new StreamWriter(@"../../../resources/professors.txt"))
            {
                foreach (Professor professor in Util.Instance.Professors)
                {
                    file.WriteLine(professor.FormatTxtFileLine());
                }
            }
        }

        public void ReadProfessors()
        {
            Util.Instance.Professors = new ObservableCollection<Professor>();
            using StreamReader file = new StreamReader(@"../../../resources/professors.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] lajs = line.Split(';');
                string[] languagesArray = lajs[3].Split(",");

                List<string> languages = languagesArray.ToList();
                RegisteredUser user = Util.Instance.Users.FirstOrDefault(c => c.ID == int.Parse(lajs[1]));
                School school = Util.Instance.Schools.FirstOrDefault(c => c.ID == int.Parse(lajs[2]));

                Util.Instance.Professors.Add(new Professor
                {
                    //1;1;1;Serbian,Russian;True
                    ID = int.Parse(lajs[0]),
                    User = user,
                    School = school,
                    Languages = languages,
                    Lessons = new List<Lesson>(),
                    Active = bool.Parse(lajs[4])
                });
            }
            file.Close();
        }
    }
}

