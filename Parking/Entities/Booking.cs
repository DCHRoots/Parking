using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
