using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.windows.ProfessorWindows;
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

namespace POP.SchoolOfForeignLanguages.windows.LessonWindows
{
    public partial class LessonDisplay : Window
    {
        ICollectionView view;
        Lesson _selected;
        public LessonDisplay()
        {
            InitializeComponent();
            UpdateView();
        }
        private void UpdateView()
        {
            ObservableCollection<Lesson> activeEntities = new ObservableCollection<Lesson>();
            foreach (Lesson lesson in Util.Instance.Lessons)
            {
                if (lesson.Active == true)
                {
                    activeEntities.Add(lesson);
                }
            }
            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                professor = x.Professor.User.Email,
                date = x.Date,
                starttime = x.StartTime,
                duration = x.Duration,
                status = x.Status,
                student = x.Student.User.Email
            }).ToList();
            DGLessons.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGLessons.IsSynchronizedWithCurrentItem = true;
            DGLessons.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGLessons_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }


        private void MIAddLesson_Click(object sender, RoutedEventArgs e)
        {
            AddEditLesson addWindow = new AddEditLesson(null);
            addWindow.Show();
        }

        private void MIEditLesson_Click(object sender, RoutedEventArgs e)
        {
            object item = DGLessons.SelectedItem;
            PropertyInfo[] props = DGLessons.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Lessons.FirstOrDefault(c => c.ID == int.Parse(studentID));

            AddEditLesson editWindow = new(_selected);
            editWindow.Show();
        }
        private void MIRemoveLesson_Click(object sender, RoutedEventArgs e)
        {
            object item = DGLessons.SelectedItem;
            PropertyInfo[] props = DGLessons.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Lessons.FirstOrDefault(c => c.ID == int.Parse(studentID));

            Util.Instance.RemoveEntity(_selected);

            UpdateView();
            view.Refresh();

        }
    }
}
