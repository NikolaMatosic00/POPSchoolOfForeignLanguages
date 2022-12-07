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

namespace POP.SchoolOfForeignLanguages.windows.StudentWindows
{
    public partial class StudentDisplay : Window
    {
        ICollectionView view;
        public StudentDisplay()
        {
            InitializeComponent();
            UpdateView();
        }

        private void UpdateView()
        {
            ObservableCollection<Student> activeEntities = new ObservableCollection<Student>();
            foreach (Student student in Util.Instance.Students)
            {
                if (student.Active == true)
                {
                    activeEntities.Add(student);
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
                emaill = x.User.Email
            }).ToList();
            DGStudents.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGStudents.IsSynchronizedWithCurrentItem = true;
            DGStudents.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGStudents_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void MIRemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            Object selected = view.CurrentItem;
            Util.Instance.RemoveEntity(selected);

            UpdateView();
            view.Refresh();

        }
    }
}

