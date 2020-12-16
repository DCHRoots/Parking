using Parking.Entities;
using Parking.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parking.Services
{
    public interface IParkingSettingService
    {
        IEnumerable<ParkingSetting> GetAll();
        ParkingSetting Create(ParkingSetting parkingSetting);
        ParkingSetting Update(ParkingSetting parkingSettingParam);
    }

    public class ParkingSettingService : IParkingSettingService
    {
        private DataContext _context;

        public ParkingSettingService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<ParkingSetting> GetAll()
        {
            return _context.ParkingSettings;
        }

        public ParkingSetting Create(ParkingSetting parkingSetting)
        {
            _context.ParkingSettings.Add(parkingSetting);
            _context.SaveChanges();

            return parkingSetting;
        }


        public ParkingSetting Update(ParkingSetting parkingSettingParam)
        {
            var parkingSetting = _context.ParkingSettings.Find(parkingSettingParam.Id);

            parkingSetting.Spaces = parkingSettingParam.Spaces;

            _context.ParkingSettings.Update(parkingSetting);
            _context.SaveChanges();

            return parkingSetting;
        }
    }
}