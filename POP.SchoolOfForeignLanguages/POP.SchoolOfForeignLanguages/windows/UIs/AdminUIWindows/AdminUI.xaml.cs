using POP.SchoolOfForeignLanguages.windows.AddressWindows;
using POP.SchoolOfForeignLanguages.windows.LessonWindows;
using POP.SchoolOfForeignLanguages.windows.ProfessorWindows;
using POP.SchoolOfForeignLanguages.windows.SchoolWindows;
using POP.SchoolOfForeignLanguages.windows.StudentWindows;
using POP.SchoolOfForeignLanguages.windows.UIs.AdminUIWindows;
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
    /// Interaction logic for AdminUI.xaml
    /// </summary>
    public partial class AdminUI : Window
    {
        public AdminUI()
        {
            InitializeComponent();
        }

        private void AddressesButton_Click(object sender, RoutedEventArgs e)
        {
            AddressDisplay addressDisplay = new AddressDisplay();
            addressDisplay.Show();
        }

        private void SchoolsButton_Click(object sender, RoutedEventArgs e)
        {
            SchoolDisplay schoolDisplay = new SchoolDisplay();
            schoolDisplay.Show();
        }

        private void StudentsButton_Click(object sender, RoutedEventArgs e)
        {
            StudentDisplay studentDisplay = new StudentDisplay();
            studentDisplay.Show();
        }

        private void ProfessorsButton_Click(object sender, RoutedEventArgs e)
        {
            ProfessorDisplay professorDisplay= new ProfessorDisplay();
            professorDisplay.Show();
        }

        private void LessonsButton_Click(object sender, RoutedEventArgs e)
        {
            LessonDisplay lessonDisplay = new LessonDisplay();
            lessonDisplay.Show();
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            AAllUsers aAllUsers= new AAllUsers();
            aAllUsers.Show();
        }
    }
}
