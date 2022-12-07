using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.windows.SchoolWindows;
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

namespace POP.SchoolOfForeignLanguages.windows.StudentWindows
{
    public partial class StudentDisplay : Window
    {
        ICollectionView view;
        Student _selected;
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
                addres = x.User.Address.Street,
                email = x.User.Email
            }).ToList();
            DGStudents.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGStudents.IsSynchronizedWithCurrentItem = true;
            DGStudents.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGStudents_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }
        private void MIAddStudent_Click(object sender, RoutedEventArgs e)
        {
            AddEditStudent addWindow = new AddEditStudent(null);
            addWindow.Show();
        }


        private void MIEditStudent_Click(object sender, RoutedEventArgs e)
        {
            object item = DGStudents.SelectedItem;
            PropertyInfo[] props = DGStudents.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Students.FirstOrDefault(c => c.ID == int.Parse(studentID));

            AddEditStudent editWindow = new(_selected);
            editWindow.Show();

        }
        private void MIRemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            object item = DGStudents.SelectedItem;
            PropertyInfo[] props = DGStudents.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Students.FirstOrDefault(c => c.ID == int.Parse(studentID));

            Util.Instance.RemoveEntity(_selected);

            UpdateView();
            view.Refresh();

        }

    }
}

