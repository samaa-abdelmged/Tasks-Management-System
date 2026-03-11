using System.ComponentModel.DataAnnotations;

namespace Tasks_Management_System.Application.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}