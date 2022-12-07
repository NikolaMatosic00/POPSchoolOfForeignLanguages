using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.models
{
    public class School
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public List<string> Languages { get; set; }

        public bool Active { get; set; }
        
        public string FormatTxtFileLine()
        {
            string languages = "";
            foreach (string language in Languages)
            {
                languages += language + ",";
            }
            return ID.ToString() + ";" + Name + ";" + Address.ID + ";" + languages.Substring(0, languages.Length - 1) + ";" + Active.ToString();
        }
    }
}
