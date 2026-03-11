using Tasks_Management_System.Domain.Entities;

public interface IJwtService
{
    string GenerateToken(ApplicationUser user);
}