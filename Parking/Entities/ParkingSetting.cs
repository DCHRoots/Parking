using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Entities
{
    public class ParkingSetting
    {
        public int Id { get; set; }
        public int Spaces { get; set; }
    }
}
