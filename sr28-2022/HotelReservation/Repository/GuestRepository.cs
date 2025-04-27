using HotelReservation.Exceptions;
using HotelReservation.Model;
using HotelReservation.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Repository
{
    public class GuestRepository : IGuestRepository
    {
        private string ToCSV(Guest guest)
        {
            return $"{guest.Id},{guest.Name},{guest.Surname},{guest.IDNumber}, {guest.reservation.Id},{guest.IsActive}"; //sta sve upisuje u fajl
        }

        private Guest FromCSV(string csv)  //ovde cita iz fajla
        {
            string[] csvValues = csv.Split(',');

            var guest = new Guest();
            guest.Id = int.Parse(csvValues[0]);
            guest.Name = csvValues[1];
            guest.Surname = csvValues[2];
            guest.IDNumber = csvValues[3];
            var reservationid = int.Parse(csvValues[4]);
            Reservation guestReservation = Hotel.GetInstance().Reservations.Find(((rt) => rt.Id == reservationid));
            guest.reservation = guestReservation;
            if(guestReservation.Guests == null)
            {
                guestReservation.Guests = new List<Guest> { };
            }
            guestReservation.Guests.Add(guest);
            guest.IsActive = bool.Parse(csvValues[5]);


            return guest;
        }

        public void Save(List<Guest> guestList)
        {
            try
            {
                using (var streamWriter = new StreamWriter("guests.txt"))
                {
                    foreach (var guest in guestList)
                    {
                        streamWriter.WriteLine(ToCSV(guest));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CouldntPersistDataException(ex.Message);
            }

        }

        public List<Guest> Load()
        {
            if (!File.Exists("guests.txt"))
            {
                return null;
            }

            try
            {
                using (var streamReader = new StreamReader("guests.txt"))
                {
                    List<Guest> guests = new List<Guest>();
                    string line;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var guest = FromCSV(line);
                        guests.Add(guest);
                    }

                    return guests;
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
