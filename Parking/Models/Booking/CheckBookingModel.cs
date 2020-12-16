using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Models.Booking
{
    public class CheckBookingModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
