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
using Parking.Models.Prices;
using Parking.Services;

namespace Parking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private IPriceService _priceService;
        private IMapper _mapper;

        public PriceController(
          IPriceService priceService,
          IMapper mapper)
        {
            _priceService = priceService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var prices = _priceService.GetAll();
            var model = _mapper.Map<IList<PriceModel>>(prices);
            return Ok(model);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]PriceModel model)
        {
            // map model to entity
            var price = _mapper.Map<Price>(model);

            try
            {
                // create user
                var response = _priceService.Create(price);
                return Ok(response);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("update")]
        public IActionResult Update([FromBody]PriceModel model)
        {
            // map model to entity
            var price = _mapper.Map<Price>(model);

            try
            {
                // create user
                var response = _priceService.Update(price);
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