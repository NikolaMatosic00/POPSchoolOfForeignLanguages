using POP.SchoolOfForeignLanguages.models.enums;
using POP.SchoolOfForeignLanguages.models;
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

namespace POP.SchoolOfForeignLanguages.windows.UIs.ProfessorUIWindows
{
    /// <summary>
    /// Interaction logic for PCreateNewLesson.xaml
    /// </summary>
    public partial class PCreateNewLesson : Window
    {
        public PCreateNewLesson()
        {
            InitializeComponent();
            datePicker.DisplayDateStart = DateTime.Now;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Professor professor = Util.Instance.findProfessorByUserID(Util.Instance.LoggedInUser.ID);
            Student student = null;

            Util.Instance.Lessons.Add(new Lesson
            {
                ID = Util.Instance.Lessons.Count + 2,
                Professor = professor,
                Date = datePicker.Text,
                StartTime = TxtStartTime.Text,
                Duration = TxtDuration.Text,
                Status = (ELessonStatus)Enum.Parse(typeof(ELessonStatus), "FREE"),
                Student = student,
                Active = true
            });

            Util.Instance.SaveEntities();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
