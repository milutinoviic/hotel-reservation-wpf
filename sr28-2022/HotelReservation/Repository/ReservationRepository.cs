using HotelReservation.Exceptions;
using HotelReservation.Model;
using HotelReservation.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Repository
{
    class ReservationRepository : IReservationRepository
        
    {
        private string ToCSV(Reservation reservation)
        {
            return  $"{reservation.Id},{reservation.Room.Id},{reservation.ReservationType},{reservation.StartDateTime},{reservation.EndDateTime},{reservation.TotalPrice},{reservation.IsActive}"; //sta mi sve upisuje u fajl
            
        }



        private Reservation FromCSV(string csv)  //ovde cita iz fajla
        {
            string[] csvValues = csv.Split(',');

            var reservation = new Reservation();
            reservation.Id = int.Parse(csvValues[0]);
            //reservation.Room = int.Parse(csvValues[1]); //////////////////////////////////////////////////////////////
            var roomId = int.Parse(csvValues[1]);
            reservation.Room = Hotel.GetInstance().Rooms.Find(r => r.Id == roomId);
            reservation.ReservationType = Enum.Parse<ReservationType>(csvValues[2]);
            reservation.StartDateTime = DateTime.Parse(csvValues[3]);
            reservation.EndDateTime = DateTime.Parse(csvValues[4]);
            reservation.TotalPrice = double.Parse(csvValues[5]);
            reservation.IsActive = bool.Parse(csvValues[6]);

            return reservation;
        }

        public void Save(List<Reservation> reservationList)
        {
            try
            {
                using (var streamWriter = new StreamWriter("reservations.txt"))
                {
                    foreach (var reservation in reservationList)
                    {
                        streamWriter.WriteLine(ToCSV(reservation));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CouldntPersistDataException(ex.Message);
            }

        }

        public List<Reservation> Load()
        {
            if (!File.Exists("reservations.txt"))
            {
                return null;
            }

            try
            {
                using (var streamReader = new StreamReader("reservations.txt"))
                {
                    List<Reservation> reservations = new List<Reservation>();
                    string line;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var reservation = FromCSV(line);
                        reservations.Add(reservation);
                    }

                    return reservations;
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

