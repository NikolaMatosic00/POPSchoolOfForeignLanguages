﻿using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.windows.StudentWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    public partial class ProfessorDisplay : Window
    {
        ICollectionView view;
        Professor _selected;
        public ProfessorDisplay()
        {
            InitializeComponent();
            UpdateView();
        }

        private void UpdateView()
        {
            ObservableCollection<Professor> activeEntities = new ObservableCollection<Professor>();
            foreach (Professor professor in Util.Instance.Professors)
            {
                if (professor.Active == true)
                {
                    activeEntities.Add(professor);
                }
            }
            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                name = x.User.Name,
                surname = x.User.Surname,
                jmbg = x.User.JMBG,
                sex = x.User.Sex,
                address = x.User.Address.Street,
                email = x.User.Email,
                school = x.School.Name,
                languages = string.Join(",", x.Languages.ToArray())
            }).ToList();
            DGProfessors.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGProfessors.IsSynchronizedWithCurrentItem = true;
            DGProfessors.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGProfessors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void MIAddProfessor_Click(object sender, RoutedEventArgs e)
        {
            AddEditProfessor addWindow = new AddEditProfessor(null);
            addWindow.Show();
        }


        private void MIEditProfessor_Click(object sender, RoutedEventArgs e)
        {
            object item = DGProfessors.SelectedItem;
            PropertyInfo[] props = DGProfessors.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Professors.FirstOrDefault(c => c.ID == int.Parse(studentID));

            AddEditProfessor editWindow = new(_selected);
            editWindow.Show();

        }

        private void MIRemoveProfessor_Click(object sender, RoutedEventArgs e)
        {
            object item = DGProfessors.SelectedItem;
            PropertyInfo[] props = DGProfessors.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Professors.FirstOrDefault(c => c.ID == int.Parse(studentID));

            Util.Instance.RemoveEntity(_selected);

            UpdateView();
            view.Refresh();

        }
    }
}

