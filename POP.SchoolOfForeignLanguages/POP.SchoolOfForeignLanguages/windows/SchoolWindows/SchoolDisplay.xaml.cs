using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.windows.AddressWindows;
using System;
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

namespace POP.SchoolOfForeignLanguages.windows.SchoolWindows
{

    public partial class SchoolDisplay : Window
    {
        ICollectionView view;
        School _selected;
        public SchoolDisplay()
        {
            InitializeComponent();
            UpdateView();
        }

        private void UpdateView()
        {
            ObservableCollection<School> activeEntities = new ObservableCollection<School>();
            foreach (School school in Util.Instance.Schools)
            {
                if (school.Active == true)
                {
                    activeEntities.Add(school);
                }
            }

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


        private void MIAddSchool_Click(object sender, RoutedEventArgs e)
        {
            AddEditSchool addWindow = new AddEditSchool(null);
            addWindow.Show();
        }


        private void MIEditSchool_Click(object sender, RoutedEventArgs e)
        {
            object item = DGSchools.SelectedItem;
            PropertyInfo[] props = DGSchools.SelectedItem.GetType().GetProperties();
            string schoolID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Schools.FirstOrDefault(c => c.ID == int.Parse(schoolID));

            AddEditSchool editWindow = new(_selected);
            editWindow.Show();

        }


        private void MIRemoveSchool_Click(object sender, RoutedEventArgs e)
        {
            object item = DGSchools.SelectedItem;
            PropertyInfo[] props = DGSchools.SelectedItem.GetType().GetProperties();
            string schoolID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Schools.FirstOrDefault(c => c.ID == int.Parse(schoolID));
            Util.Instance.RemoveEntity(_selected);

            UpdateView();
            view.Refresh();

        }

    }
}

