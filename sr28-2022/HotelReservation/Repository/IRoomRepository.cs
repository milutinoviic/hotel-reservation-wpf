using HotelReservation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Repository
{
    public interface IRoomRepository
    {

        List<Room> Load();
        void Save(List<Room> roomList);

     //   List<Room> GetAll();
     //   int Insert(Room room);
      //  void Update(Room room);
      //  void Save(List<Room> roomList);
    }
}
