﻿using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.windows.AddressWindows;
using POP.SchoolOfForeignLanguages.windows.SchoolWindows;
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

        private void AddressesButton_Click(object sender, RoutedEventArgs e)
        {
            AddressDisplay addressesDisplayWindow = new();
            addressesDisplayWindow.Show();
        }

        private void SchoolsButton_Click(Object sender, RoutedEventArgs e)
        {
            SchoolDisplay schoolsDisplayWindow = new();
            schoolsDisplayWindow.Show();
        }
    }
}
