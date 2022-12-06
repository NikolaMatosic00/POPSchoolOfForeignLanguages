using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.models
{
    internal class Professor
    {
        public int ID { get; set; }

        public RegisteredUser User{ get; set; }

        public School School { get; set; }

        public List<string> Languages { get; set; }

        public List<Lesson> Lessons { get; set; }

        public bool Active { get; set; }

    }
}
