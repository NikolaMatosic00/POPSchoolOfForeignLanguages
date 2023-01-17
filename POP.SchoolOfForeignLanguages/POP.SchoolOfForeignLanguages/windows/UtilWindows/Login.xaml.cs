using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.models.enums;
using POP.SchoolOfForeignLanguages.windows.UIs;
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

namespace POP.SchoolOfForeignLanguages.windows.UtilWindows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            Util.Instance.LoggedInUser = Util.Instance.Login(TxtJMBG.Text, TxtPassword.Password.ToString());

            if (Util.Instance.LoggedInUser != null)
            {
                switch (Util.Instance.LoggedInUser.UserType)
                {
                    case EUserType.ADMINISTRATOR:
                        AdminUI adminUI = new AdminUI();
                        this.Hide();
                        adminUI.Show();
                        break;
                    case EUserType.STUDENT:
                        StudentUI studentUI = new StudentUI();
                        this.Hide();
                        studentUI.Show();
                        break;
                    case EUserType.PROFESSOR:
                        ProfessorUI professorUI = new ProfessorUI();
                        this.Hide();
                        professorUI.Show();
                        break;
                    default:
                        MessageBox.Show("Something wrong with user type");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Login details don't match");
            }

        }
    }
}
