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
    class SchoolService
    {
        public void SaveSchools()
        {
            using (StreamWriter file = new StreamWriter(@"../../../resources/schools.txt"))
            {
                foreach (School school in Util.Instance.Schools)
                {
                    file.WriteLine(school.FormatTxtFileLine());
                }
            }
        }

        public void ReadSchools()
        {
            Util.Instance.Schools = new ObservableCollection<School>();
            using StreamReader file = new StreamReader(@"../../../resources/schools.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                
                string[] lajs = line.Split(';');
                string[] languagesArray = lajs[3].Split(",");

                List<string> languages = languagesArray.ToList();
                Address addressOfSchool = Util.Instance.Addresses.FirstOrDefault(c => c.ID == int.Parse(lajs[2]));

                Util.Instance.Schools.Add(new School
                {
                    ID = int.Parse(lajs[0]),
                    Name = lajs[1],
                    Address = addressOfSchool,
                    Languages = languages,
                    Active = bool.Parse(lajs[4])
                });



            }
            file.Close();
        }
    }
}
