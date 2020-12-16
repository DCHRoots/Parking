using Parking.Entities;
using Parking.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parking.Services
{
    public interface IPriceService
    {
        IEnumerable<Price> GetAll();
        IEnumerable<Price> GetAll(DateTime from, DateTime to);
        Price Create(Price price);
        Price Update(Price priceParam);
    }

    public class PriceService : IPriceService
    {
        private DataContext _context;

        public PriceService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Price> GetAll()
        {
            return _context.Prices;
        }

        public IEnumerable<Price> GetAll(DateTime from, DateTime to)
        {
            return _context.Prices.Where(p => p.From <= from && p.To >= to);
        }

        public Price Create(Price price)
        {
            _context.Prices.Add(price);
            _context.SaveChanges();

            return price;
        }


        public Price Update(Price priceParam)
        {
            var price = _context.Prices.Find(priceParam.Id);

            price.Value = priceParam.Value;
            price.From = priceParam.From;
            price.To = priceParam.To;
            
            _context.Prices.Update(price);
            _context.SaveChanges();

            return price;
        }
    }
}