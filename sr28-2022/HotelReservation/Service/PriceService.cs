using HotelReservation.Model;
using HotelReservation.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace HotelReservation.Service
{
    public class PriceService
    {
        public List<Price> GetAllPrices()
        {
            return Hotel.GetInstance().PriceList;
        }

        public List<Price> GetAllActivePrices()
        {
            return Hotel.GetInstance().PriceList.Where(x => x.IsActive).ToList();
        }

        public Price GetPriceByRoomTypeAndReservationType(RoomType roomType, ReservationType reservationType)
        {
            return GetAllActivePrices().Where(p => p.RoomType == roomType && p.ReservationType == reservationType).FirstOrDefault();
        }

        public void SavePrice(Price price)
        {
            if (price.Id == 0)
            {
                price.Id = GetNextIdValue();
                Hotel.GetInstance().PriceList.Add(price);
            }
            else
            {
                var index = Hotel.GetInstance().PriceList.FindIndex(r => r.Id == price.Id);
                Hotel.GetInstance().PriceList[index] = price;
            }
        }
        public int GetNextIdValue()
        {
            if (Hotel.GetInstance().PriceList.Count() == 0)
            {
                return 1;
            }
            

            return Hotel.GetInstance().PriceList.Max(r => r.Id) + 1;
        }

        public Double CaculateTotalPrice(Reservation reservation)
        {
            
            if(reservation.ReservationType == ReservationType.Day)
            {
                Price p = GetPriceByRoomTypeAndReservationType(reservation.Room.RoomType, reservation.ReservationType);
                return p == null ?  -1 :  p.PriceValue;
            }
            else
            {
                TimeSpan daysCount = reservation.EndDateTime - reservation.StartDateTime;
                Price p = GetPriceByRoomTypeAndReservationType(reservation.Room.RoomType, reservation.ReservationType);
                return p == null ? -1 : daysCount.TotalDays * p.PriceValue;

            }
        }
    }
}

