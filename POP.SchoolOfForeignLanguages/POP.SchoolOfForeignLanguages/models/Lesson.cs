using POP.SchoolOfForeignLanguages.models.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.models
{
    internal class Lesson
    {
        public int ID { get; set; }

        public Professor Professor { get; set; }

        public string Date{ get; set; }

        public string StartTime { get; set; }

        public string Duration { get; set; }

        public ELessonStatus Status { get; set; }

        public Student Student { get; set; }

        public bool Active { get; set; }

    }
}
