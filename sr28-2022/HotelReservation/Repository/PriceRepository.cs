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
        
    class PriceRepository : IPriceRepository
    {
        private string ToCSV(Price price)
        {
            return $"{price.Id},{price.RoomType.Id},{price.ReservationType},{price.PriceValue}, {price.IsActive}"; //sta sve upisuje u fajl
        }

        private Price FromCSV(string csv)  //ovde cita iz fajla
        {
           
            string[] csvValues = csv.Split(',');

            var price = new Price();
            price.Id = int.Parse(csvValues[0]);
            var priceTypeId = int.Parse(csvValues[1]);
            price.RoomType = Hotel.GetInstance().RoomTypes.Find(rt => rt.Id == priceTypeId);
            price.ReservationType = (ReservationType)Enum.Parse(typeof(ReservationType), csvValues[2]);
            price.PriceValue = double.Parse(csvValues[3]);
            price.IsActive = bool.Parse(csvValues[4]);


            return price;
        }

        public void Save(List<Price> priceList)
        {
            try
            {
                using (var streamWriter = new StreamWriter("prices.txt"))
                {
                    foreach (var price in priceList)
                    {
                        streamWriter.WriteLine(ToCSV(price));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CouldntPersistDataException(ex.Message);
            }

        }

        public List<Price> Load()
        {
            if (!File.Exists("prices.txt"))
            {
                return null;
            }

            try
            {
                using (var streamReader = new StreamReader("prices.txt"))
                {
                    List<Price> prices = new List<Price>();
                    string line;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var guest = FromCSV(line);
                        prices.Add(guest);
                    }

                    return prices;
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
