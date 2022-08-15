using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models.Dtos
{
    public class CreateCountryDTO
    {
        //public int Id { get; set; }
        [Required, StringLength(maximumLength:50, ErrorMessage ="country name is too long")]
        public string Name { get; set; }
        [Required, StringLength(maximumLength: 2, ErrorMessage = "short country name is too long")]
        public string ShortName { get; set; }
    }

    public class CountryDTO:CreateCountryDTO
    {
        public int Id { get; set; }
        public IList<HotelDTO> Hotels { get; set; }
    }
}
