using POP.SchoolOfForeignLanguages.windows.UIs.ProfessorUIWindows;
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

namespace POP.SchoolOfForeignLanguages.windows.UIs
{
    /// <summary>
    /// Interaction logic for ProfessorUI.xaml
    /// </summary>
    public partial class ProfessorUI : Window
    {
        public ProfessorUI()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PMyLessons pMyLessons = new PMyLessons();
            pMyLessons.Show();
        }
    }
}
