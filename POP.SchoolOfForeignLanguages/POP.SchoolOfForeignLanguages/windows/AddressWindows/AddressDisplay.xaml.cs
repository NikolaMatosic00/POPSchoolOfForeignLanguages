using POP.SchoolOfForeignLanguages.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP.SchoolOfForeignLanguages.windows.AddressWindows
{
    public partial class AddressDisplay : Window
    {
        ICollectionView view;
        public AddressDisplay()
        {
            InitializeComponent();
            UpdateView();
        }
        private void UpdateView()
        {
            ObservableCollection<Address> activeEntities = new ObservableCollection<Address>();
            foreach (Address address in Util.Instance.Addresses)
            {
                if (address.Active == true)
                {
                    activeEntities.Add(address);
                }
            }
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGAddresses.ItemsSource = view;
            DGAddresses.IsSynchronizedWithCurrentItem = true;
            DGAddresses.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGAddresses_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Active"))
                e.Column.Visibility = Visibility.Collapsed;
        }


        private void MIAddAddress_Click(object sender, RoutedEventArgs e)
        {
            AddEditAddress azuriranjeProzor = new AddEditAddress(null);
            azuriranjeProzor.Show();
        }


        private void MIEditAddress_Click(object sender, RoutedEventArgs e)
        {
            Address selected = view.CurrentItem as Address;

            if (selected != null)
            {
                AddEditAddress azuriranjeProzor = new AddEditAddress(selected);
                azuriranjeProzor.Show();
            }
        }


        private void MIRemoveAddress_Click(object sender, RoutedEventArgs e)
        {
            Address selected = view.CurrentItem as Address;
            Util.Instance.RemoveEntity(selected);

            UpdateView();
            view.Refresh();

        }
    }
}
