using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Parking.Controllers;
using Parking.Entities;
using Parking.Helpers;
using Parking.Models.Booking;
using Parking.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Controllers.Tests
{
    [TestClass()]
    public class BookingControllerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            var bookingService = new Mock<IBookingService>().Object;
            var parkingSettingService = new Mock<IParkingSettingService>().Object;
            var priceService = new Mock<IPriceService>().Object;
            var mapperMock = new Mock<IMapper>().Object;
        }


        [TestMethod()]
        public void GetAllTest()
        {
            var bookingService = new Mock<IBookingService>();
            var mapperMock = new Mock<IMapper>();
            
            var options = Options.Create(new AppSettings() { Secret = "abcE" });

            mapperMock.Setup(m => m.Map<Booking, BookingModel>(It.IsAny<Booking>())).Returns(new BookingModel());
            bookingService.Setup(b => b.GetAll()).Returns(new List<Booking>() { new Booking() { } });

            BookingController controller = new BookingController(null, bookingService.Object, null, mapperMock.Object, options);

            var response = controller.GetAll();

            bookingService.Verify(m => m.GetAll(), Times.Once());
            mapperMock.Verify(m => m.Map<IList<BookingModel>>(It.IsAny<IEnumerable<Booking>>()), Times.Once());
        }

        [TestMethod()]
        public void GetAllForDateTest()
        {
            var bookingServiceMock = new Mock<IBookingService>();
            var parkingSettingServiceMock = new Mock<IParkingSettingService>();

            var options = Options.Create(new AppSettings() { Secret = "abcE" });

            bookingServiceMock.Setup(b => b.GetAll(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<Booking>() { new Booking() { } });
            parkingSettingServiceMock.Setup(b => b.GetAll()).Returns(new List<ParkingSetting>() { new ParkingSetting() { Spaces = 10 } });

            BookingController controller = new BookingController(parkingSettingServiceMock.Object, bookingServiceMock.Object, null, null, options);

            var response = controller.GetAllForDate(DateTime.Now, DateTime.Now);

            bookingServiceMock.Verify(m => m.GetAll(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once());
            bookingServiceMock.Verify(m => m.GetAll(), Times.Once());
            parkingSettingServiceMock.Verify(m => m.GetAll(), Times.Once());
        }

        [TestMethod()]
        public void CheckTest()
        {
            var bookingServiceMock = new Mock<IBookingService>();
            var parkingSettingServiceMock = new Mock<IParkingSettingService>();
            var mapperMock = new Mock<IMapper>();

            var options = Options.Create(new AppSettings() { Secret = "abcE" });

            bookingServiceMock.Setup(b => b.GetAll()).Returns(new List<Booking>() { new Booking() { } });
            parkingSettingServiceMock.Setup(b => b.GetAll()).Returns(new List<ParkingSetting>() { new ParkingSetting() { Spaces = 10 } });

            BookingController controller = new BookingController(parkingSettingServiceMock.Object, bookingServiceMock.Object, null, mapperMock.Object, options);

            var response = controller.Check(new CreateBookingModel());

            bookingServiceMock.Verify(m => m.GetAll(), Times.Once());
            mapperMock.Verify(m => m.Map<IList<BookingModel>>(It.IsAny<IEnumerable<Booking>>()), Times.Once());
            bookingServiceMock.Verify(m => m.Create(It.IsAny<Booking>()), Times.Once());
            parkingSettingServiceMock.Verify(m => m.GetAll(), Times.Once());
        }

        [TestMethod()]
        public void GetForUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }
    }
}