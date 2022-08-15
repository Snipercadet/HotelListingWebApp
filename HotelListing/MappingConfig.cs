using AutoMapper;
using HotelListing.Models;
using HotelListing.Models.Dtos;

namespace HotelListing
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
        }
    }
}
