using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.models.enums;
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

namespace POP.SchoolOfForeignLanguages.windows.UIs.AdminUIWindows
{
    /// <summary>
    /// Interaction logic for AAllUsers.xaml
    /// </summary>
    public partial class AAllUsers : Window
    {
        ICollectionView view;
        string name = "";
        string surname = "";
        string email = "";
        string userType = "";
        ObservableCollection<RegisteredUser> activeEntities = new ObservableCollection<RegisteredUser>();
        public AAllUsers()
        {
            foreach (RegisteredUser user in Util.Instance.Users)
            {
                if (user.Active == true)
                {
                    activeEntities.Add(user);
                }
            }
            InitializeComponent();
            UpdateView();

            List<string> userTypesCB = new();
            userTypesCB.Add("Izaberi");
            userTypesCB.Add("ADMINISTRATOR");
            userTypesCB.Add("PROFESSOR");
            userTypesCB.Add("STUDENT");
            cmbUserType.ItemsSource = userTypesCB;
            cmbUserType.SelectedIndex = 0;
        }

        private void UpdateView()
        {
            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                name = x.Name,
                surname = x.Surname,
                jmbg = x.JMBG,
                sex = x.Sex,
                address = x.Address.Street,
                email = x.Email,
                type = x.UserType.ToString()
            }).ToList();
            DGAllUsers.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGAllUsers.IsSynchronizedWithCurrentItem = true;
            DGAllUsers.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void criteriaChanged()
        {
            activeEntities.Clear();
            ObservableCollection<RegisteredUser> prva = new ObservableCollection<RegisteredUser>();
            ObservableCollection<RegisteredUser> druga = new ObservableCollection<RegisteredUser>();
            ObservableCollection<RegisteredUser> treca = new ObservableCollection<RegisteredUser>();
            ObservableCollection<RegisteredUser> cetvrta = new ObservableCollection<RegisteredUser>();
            foreach (RegisteredUser user in Util.Instance.Users)
            {
                if (!userType.Equals("Izaberi") && user.UserType == (EUserType)Enum.Parse(typeof(EUserType), userType))
                {
                    prva.Add(user);
                }
                else if (userType.Equals("Izaberi"))
                {
                    prva.Add(user);
                }

                if (!email.Equals("") && user.Email.StartsWith(email))
                {
                    druga.Add(user);
                }
                else if (email.Equals(""))
                {
                    druga.Add(user);
                }


                if (!surname.Equals("") && user.Surname.StartsWith(surname))
                {
                    treca.Add(user);
                }
                else if (surname.Equals(""))
                {
                    treca.Add(user);
                }


                if (!name.Equals("") && user.Name.StartsWith(name))
                {
                    cetvrta.Add(user);
                }
                else if (name.Equals(""))
                {
                    cetvrta.Add(user);
                }
            }
            var commonItems = prva.Intersect(druga).Intersect(treca).Intersect(cetvrta).ToList();
            foreach (RegisteredUser hcs in commonItems)
            {
                if (!activeEntities.Contains(hcs))
                {
                    activeEntities.Add(hcs);

                }
            }
        }


        private void DGAllUsers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = txtName.Text;
            criteriaChanged();
            UpdateView();
        }


        private void txtSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            surname = txtSurname.Text;
            criteriaChanged();
            UpdateView();
        }


        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            email = txtEmail.Text;
            criteriaChanged();
            UpdateView();
        }

        private void cmbUserType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userType = cmbUserType.SelectedItem.ToString();
            criteriaChanged();
            UpdateView();
        }
    }
}
