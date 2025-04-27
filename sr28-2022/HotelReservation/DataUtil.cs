using HotelReservation.Exceptions;
using HotelReservation.Model;
using HotelReservation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation
{
    public class DataUtil
    {
        public static void LoadData()
        {
            Hotel hotel = Hotel.GetInstance();
            hotel.Id = 1;
            hotel.Name = "Hotel Park";
            hotel.Address = "Kod Futoskog parka...";

            // Može kada znamo da postoji rooms.txt datoteka
            // Ona bi trebalo da se nađe u potfolderu projektnog foldera
            // PopProjekat/bin/Debug
            try
            {
                IRoomTypeRepository roomTypeRepository = new RoomTypeRepository();
                var loadedRoomTypes = roomTypeRepository.Load();

                if (loadedRoomTypes != null)
                {
                    Hotel.GetInstance().RoomTypes = loadedRoomTypes;
                }

                IRoomRepository roomRepository = new RoomRepository();
                var loadedRooms = roomRepository.Load();

                if (loadedRooms != null)
                {
                    Hotel.GetInstance().Rooms = loadedRooms;
                }


                IUserRepository userRepository = new UserRepository();
                var loadedUsers = userRepository.Load();

                if (loadedUsers != null)
                {
                    Hotel.GetInstance().Users = loadedUsers;
                }

                

                IPriceRepository priceRepository = new PriceRepository();
                var loadedPrices = priceRepository.Load();

                if (loadedPrices != null)
                {
                    Hotel.GetInstance().PriceList = loadedPrices;
                }

                IReservationRepository reservationRepository = new ReservationRepository();
                var loadedRepository = reservationRepository.Load();

                if(loadedRepository != null)
                {
                    Hotel.GetInstance().Reservations = loadedRepository;
                }

                IGuestRepository guestRepository = new GuestRepository();
                var loadedGuests = guestRepository.Load();

                if (loadedGuests != null)
                {
                    Hotel.GetInstance().Guests = loadedGuests;
                }
                // Samo za primer...
                //BinaryRoomRepository binaryRoomRepository = new BinaryRoomRepository();
                //var loadedRoomsFromBin = binaryRoomRepository.Load();
            }
            catch (CouldntLoadResourceException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
            catch (Exception ex)
            {
                Console.Write("An unexpected error occured", ex.Message);
            }
        }

        public static void PersistData()
        {
            try
            {
                // Kada se gasi program, čuvamo u rooms.txt
                // Posle toga će rooms.txt postojati (ako nešto ne pođe po zlu)
                IRoomRepository roomRepository = new RoomRepository();
                roomRepository.Save(Hotel.GetInstance().Rooms);

                IUserRepository userRepository = new UserRepository();
                userRepository.Save(Hotel.GetInstance().Users);

                IGuestRepository guestRepository = new GuestRepository();
                guestRepository.Save(Hotel.GetInstance().Guests);

                IRoomTypeRepository roomTypeRepository = new RoomTypeRepository();
                roomTypeRepository.Save(Hotel.GetInstance().RoomTypes);

                IPriceRepository priceRepository = new PriceRepository();
                priceRepository.Save(Hotel.GetInstance().PriceList);

                IReservationRepository reservationRepository = new ReservationRepository();
                reservationRepository.Save(Hotel.GetInstance().Reservations);

                //BinaryRoomRepository binaryRoomRepository = new BinaryRoomRepository();
                //binaryRoomRepository.Save(Hotel.GetInstance().Rooms);

            }
            catch (CouldntPersistDataException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
        }
    }
}
