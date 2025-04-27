using HotelReservation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Service
{
    public class UserService
    {
        public List<User> GetAllUsers()
        {
            return Hotel.GetInstance().Users;
        }

        public List<User> GetAllActiveUsers()
        {
            return Hotel.GetInstance().Users.Where(x => x.IsActive).ToList();
        }

        public List<User> GetAllUserByUsername(string startingWith)
        {
            var users = Hotel.GetInstance().Users;
            var filteredUsers = users.FindAll((u) => u.Username.StartsWith(startingWith));
            return filteredUsers;
        }

        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                user.Id = GetNextIdValue();
                Hotel.GetInstance().Users.Add(user);
            }
            else
            {
                var index = Hotel.GetInstance().Users.FindIndex(r => r.Id == user.Id);
                Hotel.GetInstance().Users[index] = user;
            }
        }
        public int GetNextIdValue()
        {
            if (Hotel.GetInstance().Users.Count() == 0)
            {
                return 1;
            }
            
            return Hotel.GetInstance().Users.Max(r => r.Id) + 1;
        }

        public User SearchUser(string username, string password)
        {
            var users = Hotel.GetInstance().Users;

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == username && users[i].Password == password)
                {
                    return users[i];
                }
            }

            return null;
        }
    }
}
