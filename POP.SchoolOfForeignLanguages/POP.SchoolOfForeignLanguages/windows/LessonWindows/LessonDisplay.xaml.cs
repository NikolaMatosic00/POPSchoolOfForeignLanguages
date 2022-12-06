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

namespace POP.SchoolOfForeignLanguages.windows.LessonWindows
{
    public partial class LessonDisplay : Window
    {
        ICollectionView view;
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
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGLessons.ItemsSource = view;
            DGLessons.IsSynchronizedWithCurrentItem = true;
            DGLessons.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGLessons_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void MIRemoveLesson_Click(object sender, RoutedEventArgs e)
        {
            Lesson selected = view.CurrentItem as Lesson;
            Util.Instance.RemoveEntity(selected);

            UpdateView();
            view.Refresh();

        }
    }
}
