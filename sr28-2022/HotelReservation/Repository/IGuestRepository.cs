using HotelReservation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Repository
{
    internal interface IGuestRepository
    {
        List<Guest> Load();
        void Save(List<Guest> guestList);

    }
}
