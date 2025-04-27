using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Exceptions
{
    public class CouldntLoadResourceException : Exception
    {
        public CouldntLoadResourceException() { }

        public CouldntLoadResourceException(string message) : base(message) { }
    }
}
