using HotelReservation.Model;
using HotelReservation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Service
{
    public class ReservationService
    {

        IReservationRepository reservationRepository;
        public ReservationService()
        {
            reservationRepository = new ReservationRepository();
        }

        public List<Reservation> GetAllReservation()
        {
            return Hotel.GetInstance().Reservations;
        }

        public List<Reservation> GetAllActiveReservations()
        {
            return Hotel.GetInstance().Reservations.Where(x => x.IsActive).ToList();
        }

        public List<Reservation> GetAllActiveReservationsByRoom(int roomId)
        {
            return GetAllActiveReservations().Where(r => r.Room.Id == roomId).ToList();
        }

        public void SaveReservation(Reservation reservation)
        {
            if (reservation.Id == 0)
            {
                reservation.Id = GetNextIdValue();
                Hotel.GetInstance().Reservations.Add(reservation);
            }
            else
            {
                var index = Hotel.GetInstance().Reservations.FindIndex(r => r.Id == reservation.Id);
                Hotel.GetInstance().Reservations[index] = reservation;
                reservationRepository.Save(Hotel.GetInstance().Reservations);
            }
        }
        public int GetNextIdValue()
        {
            if (Hotel.GetInstance().Reservations.Count() == 0)
            {
                return 1;
            }

            return Hotel.GetInstance().Reservations.Max(r => r.Id) + 1;
        }

        public bool checkIfReservationAlreadyExistsByDateInterval(DateTime choosedStartDate, DateTime choosedEndDate, int roomId)
        {
            List<Reservation> reservations = GetAllActiveReservationsByRoom(roomId);
            return reservations.Any(r => (r.StartDateTime <= choosedStartDate && choosedStartDate <= r.EndDateTime) ||
                                    (choosedEndDate >= r.StartDateTime && choosedEndDate <= r.EndDateTime) || 
                                    (choosedStartDate <= r.StartDateTime && choosedEndDate >= r.EndDateTime) );
        }
      

    }


}


