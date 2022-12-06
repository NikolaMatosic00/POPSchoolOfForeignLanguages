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
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGProfessors.ItemsSource = view;
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
            Professor selected = view.CurrentItem as Professor;
            Util.Instance.RemoveEntity(selected);

            UpdateView();
            view.Refresh();

        }
    }
}

