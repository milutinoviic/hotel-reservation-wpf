using HotelReservation.Exceptions;
using HotelReservation.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private string ToCSV(RoomType roomType)
        {
            return $"{roomType.Id},{roomType.Name},{roomType.BedCount},{roomType.IsActive}"; //sta sve upisuje u fajl
        }

        private RoomType FromCSV(string csv)  //ovde cita iz fajla
        {
            string[] csvValues = csv.Split(',');

            var roomType = new RoomType();
            roomType.Id = int.Parse(csvValues[0]);
            roomType.Name = csvValues[1];
            roomType.BedCount = int.Parse(csvValues[2]);
            roomType.IsActive = bool.Parse(csvValues[3]);

            return roomType;
        }

        public void Save(List<RoomType> roomTypeList)
        {
            try
            {
                using (var streamWriter = new StreamWriter("roomType.txt"))
                {
                    foreach (var roomType in roomTypeList)
                    {
                        streamWriter.WriteLine(ToCSV(roomType));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CouldntPersistDataException(ex.Message);
            }

        }

        public List<RoomType> Load()
        {
            if (!File.Exists("roomType.txt"))
            {
                return null;
            }

            try
            {
                using (var streamReader = new StreamReader("roomType.txt"))
                {
                    List<RoomType> roomTypes = new List<RoomType>();
                    string line;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var roomType = FromCSV(line);
                        roomTypes.Add(roomType);
                    }

                    return roomTypes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new CouldntLoadResourceException(ex.Message);
            }
        }
    }
}
