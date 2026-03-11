using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Application.Interfaces.Tasks
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateTaskAsync(TaskItem task);

        Task UpdateTaskAsync(TaskItem task);

        Task DeleteTaskAsync(TaskItem task);

        Task<TaskItem> GetTaskByIdAsync(int id);

        Task<List<TaskItem>> GetUserTasksAsync(int userId);

        Task<List<TaskItem>> GetSectionTasksAsync(int sectionId);

        Task<List<TaskShare>> GetSharedTasksAsync(int userId);

        Task ShareTaskAsync(TaskShare share);

        Task<ApplicationUser> GetUserByEmailAsync(string email);

        Task<bool> IsTaskTitleExistsAsync(int sectionId, string title);

        Task<bool> IsTaskAlreadySharedAsync(int taskId, int targetUserId);

        Task<bool> IsTaskTitleExistsInOtherTasksAsync(int sectionId, string title, int value);
    }
}