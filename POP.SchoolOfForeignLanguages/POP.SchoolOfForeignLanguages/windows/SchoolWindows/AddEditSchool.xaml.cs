using POP.SchoolOfForeignLanguages.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace POP.SchoolOfForeignLanguages.windows.SchoolWindows
{
    /// <summary>
    /// Interaction logic for AddEditSchool.xaml
    /// </summary>
    public partial class AddEditSchool : Window
    {
        private School _school;
        public AddEditSchool(School school)
        {
            InitializeComponent();
            _school = school;
            string initialCB = "";
            List<string> addressesCB = new();
            foreach (Address one in Util.Instance.Addresses)
            {
                addressesCB.Add(one.ID + "-" + one.Street + "/" + one.StreetNumber);
                if (_school != null)
                {
                    if (one.ID == _school.Address.ID)
                    {
                        initialCB = one.ID + "-" + one.Street + "/" + one.StreetNumber;
                    }
                }
            }


            CmbAddress.ItemsSource = addressesCB;

            if (_school != null)
            {
                string languages = "";
                foreach (string language in _school.Languages)
                {
                    languages += language + ",";
                }
                this.Title = "Edit School";
                TxtName.Text = _school.Name;
                CmbAddress.Text = initialCB;
                TxtLanguages.Text = languages.Substring(0, languages.Length - 1);
            }
            else
                this.Title = "Add School";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            if (_school == null)
            {
                Address addressOfSchool = Util.Instance.Addresses.FirstOrDefault(c => c.ID == int.Parse(CmbAddress.SelectedItem.ToString().Split("-")[0]));
                string[] langugaes = TxtLanguages.Text.Split(",");
                Util.Instance.Schools.Add(new School
                {
                    ID = Util.Instance.Schools.Count + 2,
                    Name = TxtName.Text,
                    Address = addressOfSchool,
                    Languages = langugaes.ToList(),
                    Active = true
                });
            }
            else
            {
                School oldSchool = Util.Instance.Schools.FirstOrDefault(c => c.ID == int.Parse(_school.ID.ToString()));
                oldSchool.Name = TxtName.Text;
                oldSchool.Address = Util.Instance.Addresses.FirstOrDefault(c => c.ID == int.Parse(CmbAddress.SelectedItem.ToString().Split("-")[0]));
                oldSchool.Languages = TxtLanguages.Text.Split(",").ToList();
            }



            Util.Instance.SaveEntities();
            this.Close();
        }
    }
}
