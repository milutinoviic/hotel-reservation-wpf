using HotelReservation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Service
{
    public class GuestService
    {
        public List<Guest> GetAllGuests()
        {
            return Hotel.GetInstance().Guests;
        }

        public List<Guest> GetAllActiveGuests()
        {
            return Hotel.GetInstance().Guests.Where(x => x.IsActive).ToList();
        }

        public void SaveGuest(Guest guest)
        {
            if (guest.Id == 0)
            {
                guest.Id = GetNextIdValue();
                Hotel.GetInstance().Guests.Add(guest);
            }
            else
            {

                var index = Hotel.GetInstance().Guests.FindIndex(r => r.Id == guest.Id);
                Hotel.GetInstance().Guests[index] = guest;
            }
        }
        public int GetNextIdValue()
        {
            if (Hotel.GetInstance().Guests.Count() == 0)
            {
                return 1;
            }
            
            return Hotel.GetInstance().Guests.Max(r => r.Id) + 1;
        }
    }
}
