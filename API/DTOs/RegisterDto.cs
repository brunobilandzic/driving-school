using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
         [Required]
         [StringLength(12, MinimumLength = 4)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string  Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}