using Microsoft.VisualBasic;
using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.windows.SchoolWindows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace POP.SchoolOfForeignLanguages.windows.UIs.NotRegisteredUserWindows
{
    /// <summary>
    /// Interaction logic for NRUSchoolsWindow.xaml
    /// </summary>
    public partial class NRUSchoolsWindow : Window
    {
        ICollectionView view;
        string city = "";
        string language = "";
        string schoolName = "";
        string schoolAddress = "";

        ObservableCollection<School> activeEntities = new ObservableCollection<School>();
        public NRUSchoolsWindow()
        {

            foreach (School school in Util.Instance.Schools)
            {
                if (school.Active == true)
                {
                    activeEntities.Add(school);
                }
            }

            InitializeComponent();
            UpdateView();
            initializeCBs();

        }

        private void initializeCBs()
        {
            List<string> citiesCB = new();
            List<string> languagesCB = new();

            citiesCB.Add("Izaberi");
            languagesCB.Add("Izaberi");

            foreach (School one in Util.Instance.Schools)
            {
                citiesCB.Add(one.Address.City);
                foreach (string lang in one.Languages)
                {
                    languagesCB.Add(lang);
                }
            }

            CmbCity.ItemsSource = citiesCB.Distinct().ToList();
            CmbLanguage.ItemsSource = languagesCB.Distinct().ToList();

          CmbCity.SelectedIndex = 0;
          CmbLanguage.SelectedIndex = 0;
        }

        private void UpdateView()
        {

            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                name = x.Name,
                address = x.Address.Street,
                language = string.Join(",", x.Languages.ToArray())
            }).ToList();


            DGSchools.ItemsSource = itemSource;

            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGSchools.IsSynchronizedWithCurrentItem = true;
            DGSchools.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGSchools_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void criteriaChanged()
        {
            activeEntities.Clear();
            ObservableCollection<School> prva = new ObservableCollection<School>();
            ObservableCollection<School> druga = new ObservableCollection<School>();
            ObservableCollection<School> treca = new ObservableCollection<School>();
            ObservableCollection<School> cetvrta = new ObservableCollection<School>();
            foreach (School school in Util.Instance.Schools)
            {
                if (!city.Equals("Izaberi") && school.Address.City.Equals(city))
                {
                    prva.Add(school);
                }
                else if(city.Equals("Izaberi"))
                {
                    prva.Add(school);
                    Console.WriteLine(school.Name + " --- Dodata preko Izaberi city");
                }

                if (!language.Equals("Izaberi") && school.Languages.Contains(language))
                {
                    druga.Add(school);
                }
                else if (language.Equals("Izaberi"))
                {
                    druga.Add(school);
                    Console.WriteLine(school.Name + " --- Dodata preko Izaberi language");
                }

                if (!schoolName.Equals("") && school.Name.StartsWith(schoolName))
                {
                    treca.Add(school);
                }
                else if (schoolName.Equals(""))
                {
                    treca.Add(school);
                    Console.WriteLine(school.Name + " --- Dodata preko Praznog schoolName");
                }


                if (!schoolAddress.Equals("") && school.Address.Street.StartsWith(schoolAddress))
                {
                    cetvrta.Add(school);
                }
                else if (schoolAddress.Equals(""))
                {
                    cetvrta.Add(school);
                    Console.WriteLine(school.Address.Street + " --- Dodata preko Praznog schoolAddress");
                }
            }
            var commonItems = prva.Intersect(druga).Intersect(treca).Intersect(cetvrta).ToList();
            foreach(School sch in commonItems)
            {
                if (!activeEntities.Contains(sch))
                {
                activeEntities.Add(sch);

                }
            }
        }

        private void CmbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            city = CmbCity.SelectedItem.ToString();
            criteriaChanged();
            Console.WriteLine(city);
            UpdateView();
        }

        private void CmbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            language = CmbLanguage.SelectedItem.ToString();
            criteriaChanged();
            Console.WriteLine(language);
            UpdateView();
        }

        private void txtSchoolName_TextChanged(object sender, TextChangedEventArgs e)
        {
            schoolName = txtSchoolName.Text;
            criteriaChanged();
            UpdateView();
        }

        private void txtSchoolAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            schoolAddress = txtSchoolAddress.Text;
            criteriaChanged();
            UpdateView();
        }
    }
}
