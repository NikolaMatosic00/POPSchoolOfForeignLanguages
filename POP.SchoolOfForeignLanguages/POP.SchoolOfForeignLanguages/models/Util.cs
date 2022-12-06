using POP.SchoolOfForeignLanguages.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.models
{
    class Util
    {
        private static readonly Util instance = new Util();

        private AddressService _addressService;

        private SchoolService _schoolService;

        private Util()
        {
            _addressService = new AddressService();
            _schoolService = new SchoolService();
        }

        public static Util Instance
        {
            get
            {
                return instance;
            }
        }

        public ObservableCollection<Address> Addresses { get; set; }
        public ObservableCollection<School> Schools { get; set; }


        public void Initialize()
        {
            _addressService.ReadAddresses();
            _schoolService.ReadSchools();
        }

        public void saveEntities()
        {
            _addressService.saveAddresses();
            _schoolService.saveSchools();
        }

        public void removeEntity(object obj)
        {
            if (obj is Address)
            {
                Address a = (Address)obj;
                var found = Addresses.FirstOrDefault(c => c.ID == a.ID);
                found.Active = false;

            }else if(obj is School)
            {
                School a = (School)obj;
                var found = Schools.FirstOrDefault(c => c.ID == a.ID);
                found.Active = false;
            }
            saveEntities();
        }

    }

}

