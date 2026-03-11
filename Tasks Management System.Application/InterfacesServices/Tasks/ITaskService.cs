using Tasks_Management_System.Application.Common;
using Tasks_Management_System.Application.DTOs.Task;

namespace Tasks_Management_System.Application.InterfacesServices
{
    public interface ITaskService
    {
        Task<ApiResponse<TaskResponseDto>> CreateTaskAsync(int userId, CreateTaskDto dto);

        Task<ApiResponse<TaskResponseDto>> UpdateTaskAsync(int userId, UpdateTaskDto dto);

        Task<ApiResponse<string>> DeleteTaskAsync(int userId, int taskId);

        Task<ApiResponse<List<TaskResponseDto>>> GetMyTasksAsync(int userId);

        Task<ApiResponse<List<TaskResponseDto>>> GetSectionTasksAsync(int sectionId);

        Task<ApiResponse<List<TaskResponseDto>>> GetSharedTasksAsync(int userId);

        Task<ApiResponse<string>> ShareTaskAsync(int ownerId, ShareTaskDto dto);
    }
}