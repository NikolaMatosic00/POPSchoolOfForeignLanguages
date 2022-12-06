using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.models
{
    internal class Student
    {
        public int ID { get; set; }

        public RegisteredUser User { get; set; }

        public List<Lesson> Lessons { get; set; }

        public bool Active { get; set; }

        public string FormatTxtFileLine()
        {
            return ID.ToString() + ";" + User.ID.ToString() + ";" + Active.ToString();
        }
    }
}
