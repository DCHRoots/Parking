using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Models.Booking
{
    public class CreateBookingModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
