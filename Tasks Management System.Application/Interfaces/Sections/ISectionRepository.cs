using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Application.Interfaces.Sections
{
    public interface ISectionRepository
    {
        Task<Section> CreateSectionAsync(Section section);

        Task<List<Section>> GetUserSectionsAsync(int userId);

        Task<List<SectionShare>> GetSharedSectionsAsync(int userId);

        Task<Section> GetSectionByIdAsync(int sectionId);

        Task UpdateSectionAsync(Section section);

        Task DeleteSectionAsync(Section section);

        Task ShareSectionAsync(SectionShare sectionShare);

        Task<ApplicationUser> GetUserByEmailAsync(string email);

        Task<Section> GetSectionByNameAsync(int userId, string name);

        Task<bool> IsSectionSharedWithUser(int id1, int id2);
    }
}