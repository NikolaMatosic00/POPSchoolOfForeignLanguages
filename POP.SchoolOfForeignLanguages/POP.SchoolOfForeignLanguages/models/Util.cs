using POP.SchoolOfForeignLanguages.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.models
{
    class Util
    {
        private static readonly Util instance = new Util();

        private AddressService _addressService;

        private SchoolService _schoolService;

        private UserService _userService;

        private StudentService _studentService;

        private ProfessorService _professorService;

        private LessonService _leassonService;

        private Util()
        {
            _addressService = new AddressService();
            _schoolService = new SchoolService();
            _userService = new UserService();
            _studentService = new StudentService();
            _professorService = new ProfessorService();
            _leassonService = new LessonService();
        }

        public static Util Instance
        {
            get
            {
                return instance;
            }
        }

        public ObservableCollection<Address> Addresses { get; set; }
        public ObservableCollection<School> Schools { get; set; }
        public ObservableCollection<RegisteredUser> Users { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Professor> Professors { get; set; }
        public ObservableCollection<Lesson> Lessons { get; set; }


        public void Initialize()
        {
            _addressService.ReadAddresses();
            _schoolService.ReadSchools();
            _userService.ReadUsers();
            _studentService.ReadStudents();
            _professorService.ReadProfessors();
            _leassonService.ReadLessons();
        }

        public void SaveEntities()
        {
            _addressService.SaveAddresses();
            _schoolService.SaveSchools();
            _userService.SaveUsers();
            _studentService.SaveStudents();
            _professorService.SaveProfessors();
            _leassonService.SaveLessons();
        }

        public void RemoveEntity(object obj)
        {
            if (obj is Address)
            {
                Address a = (Address)obj;
                var found = Addresses.FirstOrDefault(c => c.ID == a.ID);
                found.Active = false;

            }else if(obj is School)
            {
                School a = (School)obj;
                var found = Schools.FirstOrDefault(c => c.ID == a.ID);
                found.Active = false;
            }
            else if (obj is Student)
            {
                Student a = (Student)obj;
                var found = Students.FirstOrDefault(c => c.ID == a.ID);
                found.Active = false;
            }
            else if (obj is Professor)
            {
                Professor a = (Professor)obj;
                var found = Professors.FirstOrDefault(c => c.ID == a.ID);
                found.Active = false;
            }
            else if (obj is Lesson)
            {
                Lesson a = (Lesson)obj;
                var found = Lessons.FirstOrDefault(c => c.ID == a.ID);
                found.Active = false;
            }
            SaveEntities();
        }

    }

}

