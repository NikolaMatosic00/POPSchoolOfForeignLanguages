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

namespace POP.SchoolOfForeignLanguages.windows.SchoolWindows
{
    /// <summary>
    /// Interaction logic for SchoolDisplay.xaml
    /// </summary>
    public partial class SchoolDisplay : Window
    {
        ICollectionView view;
        public SchoolDisplay()
        {
            InitializeComponent();
            UpdateView();
        }
        private bool CustomFilter(object obj)
        {
            /* Adresa adresa = obj as Adresa;


             if (adresa.Aktivan)
             {
                 if (TxtPretraga.Text != "")
                 {
                     return adresa.Ulica.Contains(TxtPretraga.Text);
                 }
                 else
                     return true;
             }*/

            return false;
        }

        private void TxtPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void UpdateView()
        {
            ObservableCollection<School> activeEntities = new ObservableCollection<School>();
            foreach (School school in Util.Instance.Schools)
            {
                if (school.Active == true)
                {
                    activeEntities.Add(school);
                }
            }
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGSchools.ItemsSource = view;
            DGSchools.IsSynchronizedWithCurrentItem = true;
            DGSchools.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGSchools_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }


        private void MIDodajAdresu_Click(object sender, RoutedEventArgs e)
        {
            /*Adresa novaAdresa = new Adresa();

            DodajAzurirajAdresu dodajProzor = new DodajAzurirajAdresu(novaAdresa);

            this.Hide();
            if ((bool)dodajProzor.ShowDialog())
            {
                //view.Refresh();
            }
            this.Show();*/
        }


        private void MIIzmeniAdresu_Click(object sender, RoutedEventArgs e)
        {
            /*  Adresa selektovanaAdresa = view.CurrentItem as Adresa;

              if (selektovanaAdresa != null)
              {
                  Adresa old = (Adresa)selektovanaAdresa.Clone();
                  DodajAzurirajAdresu azuriranjeProzor = new DodajAzurirajAdresu(selektovanaAdresa, EStatus.Izmeni);
                  if (azuriranjeProzor.ShowDialog() != true)
                  {
                      int index = Util.Instanca.Adrese.IndexOf(selektovanaAdresa);
                      Util.Instanca.Adrese[index] = old;
                  }
              }*/
        }


        private void MIObrisiAdresu_Click(object sender, RoutedEventArgs e)
        {
            School selectedSchool = view.CurrentItem as School;
            Util.Instance.removeEntity(selectedSchool);

            UpdateView();
            view.Refresh();

        }
    }
}

