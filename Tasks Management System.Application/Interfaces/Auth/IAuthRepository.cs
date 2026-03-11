using Microsoft.AspNetCore.Identity;
using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Application.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password);

        Task<ApplicationUser> GetUserByEmailAsync(string email);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        Task LogoutAsync();
    }
}