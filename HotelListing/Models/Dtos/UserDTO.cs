using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models.Dtos
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "your password is limited to {2} to {1} characters", MinimumLength = 8)]
        public string Password { get; set; }
    }
    public class UserDTO:LoginDTO
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public ICollection<string> Roles { get; set; }
       

    }
}
