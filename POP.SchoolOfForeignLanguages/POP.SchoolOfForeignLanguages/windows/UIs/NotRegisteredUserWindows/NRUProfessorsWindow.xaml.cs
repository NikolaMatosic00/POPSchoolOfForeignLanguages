using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.windows.ProfessorWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
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
    /// Interaction logic for NRUProfessorsWindow.xaml
    /// </summary>
    public partial class NRUProfessorsWindow : Window
    {
        ICollectionView view;
        int schoolId = 0;

        public NRUProfessorsWindow()
        {
            InitializeComponent();
            UpdateView();
            List<string> schoolsCB = new();
            schoolsCB.Add("0---Izaberi");
            foreach (School one in Util.Instance.Schools)
            {
                schoolsCB.Add(one.ID + "---" + one.Name);

            }
            CmbSchool.ItemsSource = schoolsCB;
            CmbSchool.SelectedIndex = 0;
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
            if(schoolId != 0)
            {
                itemSource = itemSource.Where(x => x.school.Equals(CmbSchool.SelectedItem.ToString().Split("---")[1])).ToList();
            }
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

        private void CmbSchool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            schoolId = int.Parse(CmbSchool.SelectedItem.ToString().Split("---")[0]);
            UpdateView();
        }
    }
}
