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

namespace POP.SchoolOfForeignLanguages.windows.ProfessorWindows
{
    public partial class ProfessorDisplay : Window
    {
        ICollectionView view;
        public ProfessorDisplay()
        {
            InitializeComponent();
            UpdateView();
        }

        private void UpdateView()
        {
            ObservableCollection<Professor> activeEntities = new ObservableCollection<Professor>();
            foreach (Professor professor in Util.Instance.Professors)
            {
                if (professor.Active == true)
                {
                    activeEntities.Add(professor);
                }
            }
            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                name = x.User.Name,
                surname = x.User.Surname,
                jmbg = x.User.JMBG,
                sex = x.User.Sex,
                address = x.User.Address.Street,
                email = x.User.Email,
                school = x.School.Name,
                languages = string.Join(",", x.Languages.ToArray())
            }).ToList();
            DGProfessors.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGProfessors.IsSynchronizedWithCurrentItem = true;
            DGProfessors.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGProfessors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void MIRemoveProfessor_Click(object sender, RoutedEventArgs e)
        {
            object selected = view.CurrentItem;
            Util.Instance.RemoveEntity(selected);

            UpdateView();
            view.Refresh();

        }
    }
}

