using Parking.Entities;
using Parking.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parking.Services
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetAll();
        IEnumerable<Booking> GetAll(int userId);
        IEnumerable<Booking> GetAll(DateTime from, DateTime to);
        Booking Create(Booking booking);
        Booking Update(Booking bookingParam);
        void Delete(int id);
    }

    public class BookingService : IBookingService
    {
        private DataContext _context;

        public BookingService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Bookings;
        }

        public IEnumerable<Booking> GetAll(int userId)
        {
            return _context.Bookings.Where(b => b.UserId == userId);
        }
        public IEnumerable<Booking> GetAll(DateTime from, DateTime to)
        {
            return _context.Bookings.Where(b => b.From <= from && b.To >= to);
        }

        public Booking Create(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return booking;
        }


        public Booking Update(Booking bookingParam)
        {
            var booking = _context.Bookings.Find(bookingParam.Id);

            booking.From = bookingParam.From;
            booking.To = bookingParam.To;
            
            _context.Bookings.Update(booking);
            _context.SaveChanges();

            return booking;
        }

        public void Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            _context.Bookings.Remove(booking);
            _context.SaveChanges();

        }

    }
}