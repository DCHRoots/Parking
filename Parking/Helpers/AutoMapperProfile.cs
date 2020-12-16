using AutoMapper;
using Parking.Entities;
using Parking.Models.Booking;
using Parking.Models.ParkingSetting;
using Parking.Models.Prices;
using Parking.Models.Users;

namespace Parking.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();

            CreateMap<ParkingSetting, ParkingSettingModel>();
            CreateMap<ParkingSettingModel, ParkingSetting>();
            CreateMap<CreateParkingSettingModel, ParkingSetting>();

            CreateMap<Price, PriceModel>();
            CreateMap<PriceModel, Price>();

            CreateMap<Booking, BookingModel>();
            CreateMap<BookingModel, Booking>();
            CreateMap<CreateBookingModel, Booking>();
            CreateMap<Booking, CheckBookingModel>();
        }
    }
}