using Tasks_Management_System.Application.Common;
using Tasks_Management_System.Application.DTOs.Section;

namespace Tasks_Management_System.Application.InterfacesServices
{
    public interface ISectionService
    {
        Task<ApiResponse<SectionResponseDto>> CreateSectionAsync(int userId, CreateSectionDto dto);

        Task<ApiResponse<List<SectionResponseDto>>> GetMySectionsAsync(int userId);

        Task<ApiResponse<List<SectionResponseDto>>> GetSharedSectionsAsync(int userId);

        Task<ApiResponse<SectionResponseDto>> UpdateSectionAsync(int userId, UpdateSectionDto dto);

        Task<ApiResponse<string>> DeleteSectionAsync(int userId, int sectionId);

        Task<ApiResponse<string>> ShareSectionAsync(int ownerId, ShareSectionDto dto);
    }
}