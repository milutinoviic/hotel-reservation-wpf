using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Model
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IDNumber { get; set; }

        public Reservation reservation { get; set; }

        public bool IsActive { get; set; } = true;

        public override string ToString()
        {
            return $"{Name}{Surname}{IDNumber}";
        }
        public Guest Clone()
        {
            var clone = new Guest();
            clone.Id = Id;
            clone.Name = Name;
            clone.Surname = Surname;
            clone.IDNumber = IDNumber;
            clone.reservation = reservation;
            clone.IsActive = IsActive;



            return clone;

        }
    }
}
