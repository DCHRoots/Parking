using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Models.Prices
{
    public class PriceModel
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
