using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelReservation.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public ReservationType ReservationType { get; set; }
        public List<Guest> Guests { get; set; }
        public DateTime StartDateTime { get; set; } 
        public DateTime EndDateTime { get; set; } 

        public double TotalPrice { get; set; }

        public Room Room { get; set; }
        public bool IsActive { get; set; } = true;

        public override string ToString()
        {
            return $"{Id}{Room}{ReservationType}{StartDateTime}{EndDateTime}{TotalPrice}";
        }



        public Reservation Clone()
        {
            var clone = new Reservation();
            clone.Id = Id;
            clone.Room = Room;
            clone.ReservationType = ReservationType;
            clone.StartDateTime = StartDateTime;
            clone.EndDateTime = EndDateTime;
            clone.TotalPrice = TotalPrice;
            clone.IsActive = IsActive;
            clone.Guests = Guests;
            


            return clone;

        }

    }

}
