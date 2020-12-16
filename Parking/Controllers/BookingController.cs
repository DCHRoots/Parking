using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Parking.Entities;
using Parking.Helpers;
using Parking.Models.Booking;
using Parking.Models.ParkingSetting;
using Parking.Models.Prices;
using Parking.Services;

namespace Parking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private IParkingSettingService _parkingSettingService;
        private IBookingService _bookingService;
        private IPriceService _priceService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public BookingController(
            IParkingSettingService parkingSettingService,
            IBookingService bookingService,
            IPriceService priceService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _parkingSettingService = parkingSettingService;
            _bookingService = bookingService;
            _priceService = priceService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _bookingService.GetAll();
            var model = _mapper.Map<IList<BookingModel>>(bookings);
            return Ok(model);
        }

        [HttpGet("check")]
        public IActionResult GetAllForDate(DateTime from, DateTime to)
        {
            var parkingSetting = _parkingSettingService.GetAll().First();

            List<BookingCheckModel> responseAvailableSpaces = new List<BookingCheckModel>();

            for (var i = from; i <= to; i = i.AddDays(1))
            {
                var usedSpaces = _bookingService.GetAll(i.Date, i.Date).Count();
                responseAvailableSpaces.Add(new BookingCheckModel() { AvailableSpaces = parkingSetting.Spaces - usedSpaces, Date = i });
            }

            return Ok(responseAvailableSpaces);
        }

        [HttpPut]
        public IActionResult Check([FromBody]CreateBookingModel model)
        {
            try
            {
                Booking booking = CreateBooking(model);

                var responseBooking = _mapper.Map<CheckBookingModel>(booking);

                return Ok(responseBooking);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("user")]
        public IActionResult GetForUser()
        {

            string authHeaderValue = Request.Headers["Authorization"];
            var tokenClaims = SecurityClaimsHelper.GetClaims(authHeaderValue.Substring("Bearer ".Length).Trim(), _appSettings.Secret);
            var userId = tokenClaims.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;

            var prices = _bookingService.GetAll(int.Parse(userId));
            var model = _mapper.Map<IList<BookingModel>>(prices);
            return Ok(model);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]CreateBookingModel model)
        {
            Booking booking = CreateBooking(model);

            try
            {
                var parkingSetting = _parkingSettingService.GetAll().First();

                for (var i = model.From; i <= model.To; i = i.AddDays(1))
                {
                    var usedSpaces = _bookingService.GetAll(i.Date, i.Date).Count();
                    if(usedSpaces == parkingSetting.Spaces)
                    {
                        return BadRequest(new { message = "No Available Spaces" });
                    }
                }

                var response = _bookingService.Create(booking);

                var responseBooking = _mapper.Map<BookingModel>(booking);

                return Ok(responseBooking);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        private Booking CreateBooking(CreateBookingModel model)
        {
            // map model to entity
            var booking = _mapper.Map<Booking>(model);

            // get current user id
            string authHeaderValue = Request.Headers["Authorization"];
            var tokenClaims = SecurityClaimsHelper.GetClaims(authHeaderValue.Substring("Bearer ".Length).Trim(), _appSettings.Secret);
            var userId = tokenClaims.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
            booking.UserId = int.Parse(userId);

            // get price for the period
            decimal totalPrice = 0;
            for (var i = booking.From; i <= booking.To; i = i.AddDays(1))
            {
                var price = _priceService.GetAll(i.Date, i.Date).FirstOrDefault();
                totalPrice += price == null ? 0 : price.Value;
            }
            booking.TotalPrice = totalPrice;
            return booking;
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody]BookingModel model)
        {
            // map model to entity
            var price = _mapper.Map<Booking>(model);

            try
            {
                // create user
                var booking = _bookingService.Update(price);
                var response = _mapper.Map<BookingModel>(booking);
                return Ok(response);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // create user
                _bookingService.Delete(id);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}