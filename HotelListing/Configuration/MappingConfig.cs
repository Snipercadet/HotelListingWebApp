using AutoMapper;
using HotelListing.Models;
using HotelListing.Models.Dtos;

namespace HotelListing.Configuration
{
    public class MappingConfig /*:Profile*/
    {
        //public MappingConfig()
        //{
        //    CreateMap<Country, CountryDTO>().ReverseMap();
        //    CreateMap<Country, CreateCountryDTO>().ReverseMap();
        //    CreateMap<Hotel, HotelDTO>().ReverseMap();
        //    CreateMap<Hotel, HotelDTO>().ReverseMap();
        //}
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(x =>
            {
                x.CreateMap<Country, CountryDTO>().ReverseMap();
                x.CreateMap<Country, CreateCountryDTO>().ReverseMap();
                x.CreateMap<Hotel, HotelDTO>().ReverseMap();
                x.CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
                x.CreateMap<ApiUser, UserDTO>().ReverseMap();
                x.CreateMap<ApiUser, TokenDTO>().ReverseMap();
            });

            return mappingConfig;
        }

    }
}
