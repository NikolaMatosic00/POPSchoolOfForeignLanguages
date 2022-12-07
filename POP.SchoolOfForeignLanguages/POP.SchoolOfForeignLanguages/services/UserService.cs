using POP.SchoolOfForeignLanguages.models;
using POP.SchoolOfForeignLanguages.models.enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace POP.SchoolOfForeignLanguages.services
{
    internal class UserService
    {
        public void SaveUsers()
        {
            using (StreamWriter file = new StreamWriter(@"../../../resources/users.txt"))
            {
                foreach (RegisteredUser user in Util.Instance.Users)
                {
                    file.WriteLine(user.FormatTxtFileLine());
                }
            }
        }

        public void ReadUsers()
        {
            Util.Instance.Users = new ObservableCollection<RegisteredUser>();
            using StreamReader file = new StreamReader(@"../../../resources/users.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] lajs = line.Split(';');
                Address addressOfUser = Util.Instance.Addresses.FirstOrDefault(c => c.ID == int.Parse(lajs[5]));
                Util.Instance.Users.Add(new RegisteredUser
                {
                    ID = int.Parse(lajs[0]),
                    Name = lajs[1],
                    Surname = lajs[2],
                    JMBG = lajs[3],
                    Sex = (ESex)Enum.Parse(typeof(ESex), lajs[4]),
                    Address = addressOfUser,
                    Email = lajs[6],
                    Password = lajs[7],
                    UserType = (EUserType)Enum.Parse(typeof(EUserType), lajs[8]),
                    Active = bool.Parse(lajs[9])
                });
                Console.WriteLine("USER---" + line);
            }
            file.Close();
        }
    }
}
