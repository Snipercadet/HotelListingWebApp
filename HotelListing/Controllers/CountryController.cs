using AutoMapper;
using HotelListing.Data.Repository;
using HotelListing.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;
        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAll();
                var result = _mapper.Map<List<CountryDTO>>(countries);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"something went wrong in the{nameof(GetCountries)}");
                return StatusCode(500, "Internal Server Error. Please try again");
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetCountry(int Id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(x=>x.Id==Id, new List<string> {"Hotels"});
                var result = _mapper.Map<CountryDTO>(country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"something went wrong in the{nameof(GetCountry)}");
                return StatusCode(500, "Internal Server Error. Please try again");
            }
        }
    }
}
