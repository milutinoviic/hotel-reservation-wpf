using HotelReservation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Service
{
    public class RoomTypeService
    {
        public List<RoomType> GetAllRoomTypes()
        {
            return Hotel.GetInstance().RoomTypes;
        }

        public List<RoomType> GetAllActiveRoomTypes()
        {
            return Hotel.GetInstance().RoomTypes.Where(x => x.IsActive).ToList();
        }

        public void SaveRoomType(RoomType roomType)
        {
            if (roomType.Id == 0)
            {
                roomType.Id = GetNextIdValue();
                Hotel.GetInstance().RoomTypes.Add(roomType);
            }
            else
            {
                var index = Hotel.GetInstance().RoomTypes.FindIndex(r => r.Id == roomType.Id);
                Hotel.GetInstance().RoomTypes[index] = roomType;
            }
        }
        public int GetNextIdValue()
        {
            if (Hotel.GetInstance().RoomTypes.Count() == 0)
            {
                return 1;
            }
            
            return Hotel.GetInstance().RoomTypes.Max(r => r.Id) + 1;
        }
    }
}
