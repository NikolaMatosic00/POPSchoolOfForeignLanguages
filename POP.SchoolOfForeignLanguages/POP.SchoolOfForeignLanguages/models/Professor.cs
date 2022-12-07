using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.models
{
    public class Professor
    {
        public int ID { get; set; }

        public RegisteredUser User{ get; set; }

        public School School { get; set; }

        public List<string> Languages { get; set; }

        public List<Lesson> Lessons { get; set; }

        public bool Active { get; set; }

        public string FormatTxtFileLine()
        {
            string languages = "";
            foreach (string language in Languages)
            {
                languages += language + ",";
            }
            return ID.ToString() + ";" + User.ID.ToString() + ";" + School.ID.ToString() + ";" + languages.Substring(0, languages.Length - 1) + ";" + Active.ToString();
        }
    }
}
