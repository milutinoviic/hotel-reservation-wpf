using HotelReservation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Repository
{
    
    internal interface IPriceRepository
    {
        List<Price> Load();
        void Save(List<Price> priceList);

    }
}
