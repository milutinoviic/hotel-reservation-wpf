using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Model
{
    [Serializable]
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BedCount { get; set; }
        public bool IsActive { get; set; } = true;
        public override string ToString()
        {
            return Name;
        }

        public RoomType Clone()
        {
            var clone = new RoomType();
            clone.Id = Id;
            clone.Name = Name;
            clone.IsActive = IsActive;
            clone.BedCount = BedCount;
            return clone;

        }
    }
}
