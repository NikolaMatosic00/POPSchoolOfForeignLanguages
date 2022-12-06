using System;
using System.Collections.Generic;
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
    }
}
