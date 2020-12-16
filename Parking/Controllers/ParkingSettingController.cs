using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Parking.Entities;
using Parking.Helpers;
using Parking.Models.ParkingSetting;
using Parking.Services;

namespace Parking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ParkingSettingController : ControllerBase
    {
        private IParkingSettingService _parkingSettingService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ParkingSettingController(
          IParkingSettingService parkingSettingService,
          IMapper mapper,
          IOptions<AppSettings> appSettings)
        {
            _parkingSettingService = parkingSettingService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var parkings = _parkingSettingService.GetAll();
            var model = _mapper.Map<IList<ParkingSettingModel>>(parkings);
            return Ok(model);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]CreateParkingSettingModel model)
        {
            // map model to entity
            var parking = _mapper.Map<ParkingSetting>(model);

            try
            {
                // create user
                var response = _parkingSettingService.Create(parking);
                return Ok(response);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("update")]
        public IActionResult Update([FromBody]ParkingSettingModel model)
        {
            // map model to entity
            var parking = _mapper.Map<ParkingSetting>(model);

            try
            {
                // create user
                var response = _parkingSettingService.Update(parking);
                return Ok(response);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}