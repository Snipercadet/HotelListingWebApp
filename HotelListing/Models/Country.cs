using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Models
{
    [Table("Countries")]
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
