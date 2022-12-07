using POP.SchoolOfForeignLanguages.models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP.SchoolOfForeignLanguages.windows.AddressWindows
{

    public partial class AddEditAddress : Window
    {

        private Address _address;
        public AddEditAddress(Address address)
        {
            InitializeComponent();
            _address = address;

            if (_address != null)
            {
                this.Title = "Izmeni adresu";
                TxtStreet.Text = _address.Street;
                TxtStreetNumber.Text = _address.StreetNumber.ToString();
                TxtCity.Text = _address.City;
                TxtCountry.Text = _address.Country;
            }
            else
                this.Title = "Dodaj adresu";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            if (_address == null)
            {
                Util.Instance.Addresses.Add(new Address
                {
                    ID = Util.Instance.Addresses.Count() + 2,
                    Street = TxtStreet.Text,
                    StreetNumber = int.Parse(TxtStreetNumber.Text),
                    City = TxtCity.Text,
                    Country = TxtCountry.Text,
                    Active = true
                });
            }
            else
            {
                Address oldAddress = Util.Instance.Addresses.FirstOrDefault(c => c.ID == int.Parse(_address.ID.ToString()));
                oldAddress.Street = TxtStreet.Text;
                oldAddress.StreetNumber = int.Parse(TxtStreetNumber.Text);
                oldAddress.City = TxtCity.Text;
                oldAddress.Country = TxtCountry.Text;
            }



            Util.Instance.SaveEntities();
            this.Close();
        }
    }
}
