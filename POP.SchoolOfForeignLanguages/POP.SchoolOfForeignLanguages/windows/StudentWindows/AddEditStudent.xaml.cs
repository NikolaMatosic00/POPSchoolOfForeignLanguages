using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.models.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            CmbSex.ItemsSource = new[] { "MALE", "FEMALE" };

            if (_student != null)
            {

                this.Title = "Edit Student";
                TxtName.Text = _student.User.Name;
                TxtSurname.Text = _student.User.Surname;
                TxtJMBG.Text = _student.User.JMBG;
                CmbSex.Text = _student.User.Sex.ToString();
                TxtStreet.Text = _student.User.Address.Street;
                TxtStreetNumber.Text = _student.User.Address.StreetNumber.ToString();
                TxtCity.Text = _student.User.Address.City;
                TxtCountry.Text = _student.User.Address.Country;
                TxtEmail.Text = _student.User.Email;
                TxtPassword.Password = _student.User.Password;
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
                Address addressOfStudent = new Address
                {
                    ID = Util.Instance.Addresses.Count() + 2,
                    Street = TxtStreet.Text,
                    StreetNumber = int.Parse(TxtStreetNumber.Text),
                    City = TxtCity.Text,
                    Country = TxtCountry.Text,
                    Active = true
                };

                Util.Instance.Addresses.Add(addressOfStudent);

                RegisteredUser user = new RegisteredUser
                {
                    ID = Util.Instance.Users.Count + 2,
                    Name = TxtName.Text,
                    Surname = TxtSurname.Text,
                    JMBG = TxtJMBG.Text,
                    Sex = (ESex)Enum.Parse(typeof(ESex), CmbSex.Text),
                    Address = addressOfStudent,
                    Email = TxtEmail.Text,
                    Password = TxtPassword.Password.ToString(),
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
                oldStudent.User.Password = TxtPassword.Password.ToString();
                oldStudent.User.Address.Street = TxtStreet.Text;
                oldStudent.User.Address.StreetNumber = int.Parse(TxtStreetNumber.Text);
                oldStudent.User.Address.City = TxtCity.Text;
                oldStudent.User.Address.Country = TxtCountry.Text;
            }



            Util.Instance.SaveEntities();
            this.Close();
        }

    }
}
