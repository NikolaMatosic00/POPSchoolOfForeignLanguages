using POP.SchoolOfForeignLanguages.models.enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.models
{
    internal class RegisteredUser
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string JMBG { get; set; }

        public ESex Sex { get; set; }

        public Address Address { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public EUserType UserType { get; set; }

        public bool Active { get; set; }

        public string FormatTxtFileLine()
        {
            return ID.ToString() + ";" + Name + ";" + Surname + ";" + JMBG + ";" + Sex.ToString() + ";" + Address.ID.ToString() + ";" + Email + ";"
                + Password + ";" + UserType.ToString() + ";" + Active.ToString();
        }
    }
}
