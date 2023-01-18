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

namespace POP.SchoolOfForeignLanguages.windows.ProfessorWindows
{
    /// <summary>
    /// Interaction logic for AddEditProfessor.xaml
    /// </summary>
    public partial class AddEditProfessor : Window
    {
        private Professor _professor;
        public AddEditProfessor(Professor professor)
        {
            InitializeComponent();
            _professor = professor;

            string initialAddressesCB = "";
            string initialSchoolsCB = "";
            List<string> addressesCB = new();
            foreach (Address one in Util.Instance.Addresses)
            {
                addressesCB.Add(one.ID + "-" + one.Street + "/" + one.StreetNumber);
                Console.WriteLine(one.ID + "-" + one.Street + "/" + one.StreetNumber);
                if (_professor != null)
                {
                    if (one.ID == _professor.User.Address.ID)
                    {
                        initialAddressesCB = one.ID + "-" + one.Street + "/" + one.StreetNumber;
                    }
                }
            }
            CmbAddress.ItemsSource = addressesCB;
            CmbSex.ItemsSource = new[] { "MALE", "FEMALE" };
            List<string> schoolsCB = new();
            foreach (School one in Util.Instance.Schools)
            {
                schoolsCB.Add(one.ID + "-" + one.Name);
                Console.WriteLine(one.ID + "-" + one.Name);
                if (_professor != null)
                {
                    if (one.ID == _professor.School.ID)
                    {
                        initialSchoolsCB = one.ID + "-" + one.Name;
                    }
                }
            }
            CmbSchool.ItemsSource = schoolsCB;

            if (_professor != null)
            {
                string languages = "";
                foreach (string language in _professor.Languages)
                {
                    languages += language + ",";
                }

                this.Title = "Edit Professor";
                TxtName.Text = _professor.User.Name;
                TxtSurname.Text = _professor.User.Surname;
                TxtJMBG.Text = _professor.User.JMBG;
                CmbSex.Text = _professor.User.Sex.ToString();
                CmbSchool.Text = initialSchoolsCB;
                CmbAddress.Text = initialAddressesCB;
                TxtEmail.Text = _professor.User.Email;
                TxtPassword.Text = _professor.User.Password;
                TxtLanguages.Text = languages.Substring(0, languages.Length - 1);
            }
            else
            {

                this.Title = "Add Professor";
            }

        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            if (_professor == null)
            {
                Address addressOfProfessor = Util.Instance.Addresses.FirstOrDefault(c => c.ID == int.Parse(CmbAddress.SelectedItem.ToString().Split("-")[0]));
                School schoolOfProfessor = Util.Instance.Schools.FirstOrDefault(c => c.ID == int.Parse(CmbSchool.SelectedItem.ToString().Split("-")[0]));

                RegisteredUser user = new RegisteredUser
                {
                    ID = Util.Instance.Users.Count + 2,
                    Name = TxtName.Text,
                    Surname = TxtSurname.Text,
                    JMBG = TxtJMBG.Text,
                    Sex = (ESex)Enum.Parse(typeof(ESex), CmbSex.Text),
                    Address = addressOfProfessor,
                    Email = TxtEmail.Text,
                    Password = TxtPassword.Text,
                    UserType = EUserType.STUDENT,
                    Active = true
                };
                Util.Instance.Users.Add(user);

                string[] langugaes = TxtLanguages.Text.Split(",");

                Util.Instance.Professors.Add(new Professor
                {
                    ID = Util.Instance.Students.Count + 2,
                    User = user,
                    School = schoolOfProfessor,
                    Languages = langugaes.ToList(),
                    Lessons = new List<Lesson>(),
                    Active = true
                });
            }
            else
            {
                Professor oldProfessor = Util.Instance.Professors.FirstOrDefault(c => c.ID == int.Parse(_professor.ID.ToString()));
                oldProfessor.User.Name = TxtName.Text;
                oldProfessor.User.Surname = TxtSurname.Text;
                oldProfessor.User.JMBG = TxtJMBG.Text;
                oldProfessor.User.Email = TxtEmail.Text;
                oldProfessor.User.Password = TxtPassword.Text;
                oldProfessor.User.Address = Util.Instance.Addresses.FirstOrDefault(c => c.ID == int.Parse(CmbAddress.SelectedItem.ToString().Split("-")[0]));
                oldProfessor.School = Util.Instance.Schools.FirstOrDefault(c => c.ID == int.Parse(CmbSchool.SelectedItem.ToString().Split("-")[0]));
                oldProfessor.Languages = TxtLanguages.Text.Split(",").ToList();
            }



            Util.Instance.SaveEntities();
            this.Close();
        }


    }
}
