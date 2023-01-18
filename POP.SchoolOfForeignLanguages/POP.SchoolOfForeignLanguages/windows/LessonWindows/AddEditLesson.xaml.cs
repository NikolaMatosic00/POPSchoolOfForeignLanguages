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
using System.Xml.Linq;

namespace POP.SchoolOfForeignLanguages.windows.LessonWindows
{
    public partial class AddEditLesson : Window
    {
        private Lesson _lesson;

        public AddEditLesson(Lesson lesson)
        {
            InitializeComponent();
            _lesson = lesson;

            string initialProfessorsCB = "";
            
            List<string> professorsCB = new();
            foreach (Professor one in Util.Instance.Professors)
            {
                professorsCB.Add(one.ID + "-" + one.User.Name + " " + one.User.Surname);
                if (_lesson != null)
                {
                    if (one.ID == _lesson.Professor.ID)
                    {
                        initialProfessorsCB = one.ID + "-" + one.User.Name + " " + one.User.Surname;
                    }
                }
            }
            CmbProfessor.ItemsSource = professorsCB;

            if (_lesson != null)
            {


                this.Title = "Edit Lesson";
                CmbProfessor.Text = initialProfessorsCB;
                datePicker.Text = _lesson.Date;
                TxtStartTime.Text = _lesson.StartTime;
                TxtDuration.Text = _lesson.Duration;
                TxtStatus.Text = _lesson.Status.ToString();
               
            }
            else
            {
                datePicker.DisplayDateStart = DateTime.Now;
                this.Title = "Add Lesson";
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            if (_lesson == null)
            {
                Professor professor = Util.Instance.Professors.FirstOrDefault(c => c.ID == int.Parse(CmbProfessor.SelectedItem.ToString().Split("-")[0]));

                Util.Instance.Lessons.Add(new Lesson
                {
                    ID = Util.Instance.Lessons.Count + 2,
                    Professor = professor,
                    Date = datePicker.Text,
                    StartTime = TxtStartTime.Text,
                    Duration = TxtDuration.Text,
                    Status = (ELessonStatus)Enum.Parse(typeof(ELessonStatus), "FREE"),
                    Student = null,
                    Active = true
                });
            }
            else
            {
                Lesson oldLesson= Util.Instance.Lessons.FirstOrDefault(c => c.ID == int.Parse(_lesson.ID.ToString()));
                oldLesson.Professor = Util.Instance.Professors.FirstOrDefault(c => c.ID == int.Parse(CmbProfessor.SelectedItem.ToString().Split("-")[0]));
                oldLesson.Date = datePicker.Text;
                oldLesson.StartTime= TxtStartTime.Text;
                oldLesson.Duration= TxtDuration.Text;
                oldLesson.Status = (ELessonStatus)Enum.Parse(typeof(ELessonStatus), TxtStatus.Text);
            }



            Util.Instance.SaveEntities();
            this.Close();
        }
    }
}
