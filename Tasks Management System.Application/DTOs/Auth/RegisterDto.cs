using System.ComponentModel.DataAnnotations;

namespace Tasks_Management_System.Application.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}