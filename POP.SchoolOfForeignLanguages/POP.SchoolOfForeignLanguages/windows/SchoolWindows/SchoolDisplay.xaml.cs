using POP.SchoolOfForeignLanguages.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
                Id = x.ID,
                Namee = x.Name,
                Address = x.Address.Street,
                Language = string.Join(",", x.Languages.ToArray())
            }).ToList();
            DGSchools.ItemsSource = itemSource;

            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGSchools.IsSynchronizedWithCurrentItem = true;
            DGSchools.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGSchools_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }


        private void MIAddSchool_Click(object sender, RoutedEventArgs e)
        {

        }


        private void MIEditSchool_Click(object sender, RoutedEventArgs e)
        {

        }


        private void MIRemoveSchool_Click(object sender, RoutedEventArgs e)
        {
            object selected = view.CurrentItem;
            Util.Instance.RemoveEntity(selected);

            UpdateView();
            view.Refresh();

        }
    }
}

