using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Models.Booking
{
    public class BookingCheckModel
    {
        public int AvailableSpaces { get; set; }
        public DateTime Date { get; set; }
    }
}
