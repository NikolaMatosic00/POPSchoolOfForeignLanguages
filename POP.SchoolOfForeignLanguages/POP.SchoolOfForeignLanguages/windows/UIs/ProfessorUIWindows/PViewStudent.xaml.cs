using POP.SchoolOfForeignLanguages.models;
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

namespace POP.SchoolOfForeignLanguages.windows.UIs.ProfessorUIWindows
{
    /// <summary>
    /// Interaction logic for PViewStudent.xaml
    /// </summary>
    public partial class PViewStudent : Window
    {
        public PViewStudent(Student _student)
        {
            InitializeComponent();
            this.Title = "View Student";
            TxtName.Text = _student.User.Name;
            TxtSurname.Text = _student.User.Surname;
            TxtJMBG.Text = _student.User.JMBG;
            TxtSex.Text = _student.User.Sex.ToString();
            TxtAddress.Text = _student.User.Address.City + ": " + _student.User.Address.Street;
            TxtEmail.Text = _student.User.Email;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
