using POP.SchoolOfForeignLanguages.windows.UIs.NotRegisteredUserWindows;
using POP.SchoolOfForeignLanguages.windows.UIs.StudentUIWindows;
using System;
using System.Collections.Generic;
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

namespace POP.SchoolOfForeignLanguages.windows.UIs
{
    /// <summary>
    /// Interaction logic for StudentUI.xaml
    /// </summary>
    public partial class StudentUI : Window
    {
        public StudentUI()
        {
            InitializeComponent();
        }

        private void SchoolsButtonClicked(object sender, RoutedEventArgs e)
        {
            NRUSchoolsWindow nruSchoolsDisplayWindow = new();
            nruSchoolsDisplayWindow.Show();
        }

        private void BookALessonButtonClicked(object sender, RoutedEventArgs e)
        {
            SBookALessonWindow sBookALessonWindow = new();
            sBookALessonWindow.Show();
        }

        private void MyBookingsButtonClicked(object sender, RoutedEventArgs e)
        {
            SMyBookings sBookMyBookingsWindow = new();
            sBookMyBookingsWindow.Show();
        }

    }
}
