using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.windows.AddressWindows;
using POP.SchoolOfForeignLanguages.windows.LessonWindows;
using POP.SchoolOfForeignLanguages.windows.ProfessorWindows;
using POP.SchoolOfForeignLanguages.windows.SchoolWindows;
using POP.SchoolOfForeignLanguages.windows.StudentWindows;
using POP.SchoolOfForeignLanguages.windows.UIs.NotRegisteredUserWindows;
using POP.SchoolOfForeignLanguages.windows.UtilWindows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP.SchoolOfForeignLanguages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Util.Instance.Initialize();
        }

        private void SchoolsButton_Click(Object sender, RoutedEventArgs e)
        {
            NRUSchoolsWindow nruSchoolsDisplayWindow = new();
            nruSchoolsDisplayWindow.Show();
        }
        private void ProfessorsButton_Click(Object sender, RoutedEventArgs e)
        {
            NRUProfessorsWindow nruProfessorsDisplayWindow = new();
            nruProfessorsDisplayWindow.Show();
        }
        private void RegistrationButton_Click(Object sender, RoutedEventArgs e)
        {
            AddEditStudent studentsDisplayWindow = new(null);
            studentsDisplayWindow.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new();
            loginWindow.Show();
        }

    }
}
