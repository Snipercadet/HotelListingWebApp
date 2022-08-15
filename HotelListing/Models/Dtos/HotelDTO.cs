using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models.Dtos
{
    public class CreateHotelDTO
    {
        //public int Id { get; set; }
        [Required, StringLength(maximumLength: 50, ErrorMessage = "hotel name is too long")]
        public string Name { get; set; }

        [Required, StringLength(maximumLength: 50, ErrorMessage = "country name is too long")]
        public string Address { get; set; }

        [Required, Range(1,5)]
        public double Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
        
    }

    public class HotelDTO : CreateHotelDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }
}
