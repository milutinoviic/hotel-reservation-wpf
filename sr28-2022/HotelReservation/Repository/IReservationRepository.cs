using HotelReservation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Repository
{
    interface IReservationRepository
    {
        List<Reservation> Load();
        void Save(List<Reservation> reservationList);
    }
}
