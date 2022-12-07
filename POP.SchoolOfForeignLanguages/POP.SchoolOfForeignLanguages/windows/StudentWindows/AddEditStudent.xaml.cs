using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.models.enums;
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

namespace POP.SchoolOfForeignLanguages.windows.StudentWindows
{
    /// <summary>
    /// Interaction logic for AddEditStudent.xaml
    /// </summary>
    public partial class AddEditStudent : Window
    {
        private Student _student;
        public AddEditStudent(Student student)
        {
            InitializeComponent();
            _student = student;

            string initialCB = "";
            List<string> addressesCB = new();
            foreach (Address one in Util.Instance.Addresses)
            {
                addressesCB.Add(one.ID + "-" + one.Street + "/" + one.StreetNumber);
                if (_student != null)
                {
                    if (one.ID == _student.User.Address.ID)
                    {
                        initialCB = one.ID + "-" + one.Street + "/" + one.StreetNumber;
                    }
                }
            }


            CmbAddress.ItemsSource = addressesCB;

            if (_student != null)
            {

                this.Title = "Edit Student";
                TxtName.Text = _student.User.Name;
                TxtSurname.Text = _student.User.Surname;
                TxtJMBG.Text = _student.User.JMBG;
                TxtSex.Text = _student.User.Sex.ToString();
                CmbAddress.Text = initialCB;
                TxtEmail.Text = _student.User.Email;
                TxtPassword.Text = _student.User.Password;
            }
            else
                this.Title = "Add Student";
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            if (_student == null)
            {
                Address addressOfStudent = Util.Instance.Addresses.FirstOrDefault(c => c.ID == int.Parse(CmbAddress.SelectedItem.ToString().Split("-")[0]));

                RegisteredUser user = new RegisteredUser
                {
                    ID = Util.Instance.Users.Count + 2,
                    Name = TxtName.Text,
                    Surname = TxtSurname.Text,
                    JMBG = TxtJMBG.Text,
                    Sex = (ESex)Enum.Parse(typeof(ESex), TxtSex.Text),
                    Address = addressOfStudent,
                    Email = TxtEmail.Text,
                    Password = TxtPassword.Text,
                    UserType = EUserType.STUDENT,
                    Active = true
                };
                Util.Instance.Users.Add(user);

                Util.Instance.Students.Add(new Student
                {
                    ID = Util.Instance.Students.Count + 2,
                    User = user,
                    Lessons = new List<Lesson>(),
                    Active = true
                });
            }
            else
            {
                Student oldStudent = Util.Instance.Students.FirstOrDefault(c => c.ID == int.Parse(_student.ID.ToString()));
                oldStudent.User.Name = TxtName.Text;
                oldStudent.User.Surname = TxtSurname.Text;
                oldStudent.User.JMBG = TxtJMBG.Text;
                oldStudent.User.Email = TxtEmail.Text;
                oldStudent.User.Password = TxtPassword.Text;
                oldStudent.User.Address = Util.Instance.Addresses.FirstOrDefault(c => c.ID == int.Parse(CmbAddress.SelectedItem.ToString().Split("-")[0]));
            }



            Util.Instance.SaveEntities();
            this.Close();
        }

    }
}
