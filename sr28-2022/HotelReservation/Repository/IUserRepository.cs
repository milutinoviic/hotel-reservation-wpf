using HotelReservation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Repository
{
    internal interface IUserRepository
    {
        List<User> Load();
        void Save(List<User> userList);

    }
}
