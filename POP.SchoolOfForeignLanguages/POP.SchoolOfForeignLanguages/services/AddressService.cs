using POP.SchoolOfForeignLanguages.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.services
{
    class AddressService
    {
        public void saveAddresses()
        {
            using (StreamWriter file = new StreamWriter(@"../../../resources/addresses.txt"))
            {
                foreach (Address address in Util.Instance.Addresses)
                {
                    file.WriteLine(address.formatTxtFileLine());
                }
            }
        }

        public void ReadAddresses()
        {
            Util.Instance.Addresses = new ObservableCollection<Address>();
            using StreamReader file = new StreamReader(@"../../../resources/addresses.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] lajs = line.Split(';');

                Util.Instance.Addresses.Add(new Address
                    {
                        ID = int.Parse(lajs[0]),
                        Street = lajs[1],
                        StreetNumber = int.Parse(lajs[2]),
                        City = lajs[3],
                        Country = lajs[4],
                        Active = bool.Parse(lajs[5])
                    });

                

            }
            file.Close();
        }
    }

}
