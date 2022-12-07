using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP.SchoolOfForeignLanguages.models
{
    public class Address
    {
        public int ID { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public bool Active { get; set; }

        public string FormatTxtFileLine()
        {
            return ID.ToString() + ";" + Street + ";" + StreetNumber.ToString() + ";" + City + ";" + Country + ";" + Active.ToString();
        }

    }

}
