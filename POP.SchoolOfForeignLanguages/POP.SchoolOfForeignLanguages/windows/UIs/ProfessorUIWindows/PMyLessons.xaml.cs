using POP.SchoolOfForeignLanguages.models.enums;
using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.windows.UIs.StudentUIWindows;
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
using System.Windows.Markup;

namespace POP.SchoolOfForeignLanguages.windows.UIs.ProfessorUIWindows
{
    /// <summary>
    /// Interaction logic for PMyLessons.xaml
    /// </summary>
    public partial class PMyLessons : Window
    {

        ICollectionView view;
        Lesson _selected;
        string selectedDate = "Izaberi";

        public PMyLessons()
        {
            InitializeComponent();
            initializeCBs();
            UpdateView();

        }

        private void initializeCBs()
        {
            List<string> datesCB = new();

            datesCB.Add("Izaberi");

            foreach (Lesson one in Util.Instance.findProfessorByUserID(Util.Instance.LoggedInUser.ID).Lessons)
            {
                datesCB.Add(one.Date);
                Console.WriteLine("Cao");
            }

            CBDate.ItemsSource = datesCB.Distinct().ToList();

            CBDate.SelectedIndex = 0;
        }

        private void UpdateView()
        {
            ObservableCollection<Lesson> activeEntities = new ObservableCollection<Lesson>();
            foreach (Lesson lesson in Util.Instance.Lessons)
            {
                if (lesson.Active == true)
                {
                    if (lesson.Professor.ID == Util.Instance.findProfessorByUserID(Util.Instance.LoggedInUser.ID).ID)
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
                student = x.Student?.User.Email ?? "it's a free class"
            }).ToList();
            if (!selectedDate.Equals("Izaberi"))
            {
                itemSource = itemSource.Where(x => x.date.Equals(selectedDate)).ToList();
            }
            DGLessons.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGLessons.IsSynchronizedWithCurrentItem = true;
            DGLessons.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGLessons_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
        }


        private void MIUnbookLesson_Click(object sender, RoutedEventArgs e)
        {
            object item = DGLessons.SelectedItem;
            PropertyInfo[] props = DGLessons.SelectedItem.GetType().GetProperties();
            string lessonID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Lessons.FirstOrDefault(c => c.ID == int.Parse(lessonID));

            _selected.Status = ELessonStatus.FREE;
            _selected.Student = null;

            Util.Instance.SaveEntities();
            var currentWindow = Window.GetWindow(this);
            currentWindow.Close();
            var newWindow = new SMyBookings();
            newWindow.Show();
        }

        private void MIRemoveLesson_Click(object sender, RoutedEventArgs e)
        {
            object item = DGLessons.SelectedItem;
            PropertyInfo[] props = DGLessons.SelectedItem.GetType().GetProperties();
            string lessonID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Lessons.FirstOrDefault(c => c.ID == int.Parse(lessonID));

            if (_selected.Status == ELessonStatus.FREE)
            {
                Util.Instance.RemoveEntity(_selected);

                var currentWindow = Window.GetWindow(this);
                currentWindow.Close();
                var newWindow = new PMyLessons();
                newWindow.Show();
            }
            else
            {
                MessageBox.Show("Lessons that are not free can't be removed", "Unable to remove lesson", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void MIViewStudent_Click(object sender, RoutedEventArgs e)
        {
            object item = DGLessons.SelectedItem;
            PropertyInfo[] props = DGLessons.SelectedItem.GetType().GetProperties();
            string lessonID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Lessons.FirstOrDefault(c => c.ID == int.Parse(lessonID));

            if (_selected.Student != null)
            {
                PViewStudent pViewStudent = new PViewStudent(_selected.Student);
                pViewStudent.Show();
            }
            else
            {
                MessageBox.Show("Choosen lesson has not been taken yet", "Lesson is free", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void MICreateLesson_Click(object sender, RoutedEventArgs e)
        {
            PCreateNewLesson pCreateNewLesson= new PCreateNewLesson();
            pCreateNewLesson.Show();
        }

        private void CBDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = CBDate.SelectedItem.ToString();
            UpdateView();
        }
    }
}
