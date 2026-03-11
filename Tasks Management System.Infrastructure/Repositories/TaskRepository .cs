using Microsoft.EntityFrameworkCore;
using Tasks_Management_System.Application.Interfaces.Tasks;
using Tasks_Management_System.Domain.Entities;
using Tasks_Management_System.Infrastructure.Data;

namespace Tasks_Management_System.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TaskItem task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TaskItem>> GetUserTasksAsync(int userId)
        {
            return await _context.Tasks
                .Include(t => t.Section)
                .Where(t => t.OwnerId == userId)
                .ToListAsync();
        }

        public async Task<List<TaskItem>> GetSectionTasksAsync(int sectionId)
        {
            return await _context.Tasks
                .Include(t => t.Section)
                .Where(t => t.SectionId == sectionId)
                .ToListAsync();
        }

        public async Task<List<TaskShare>> GetSharedTasksAsync(int userId)
        {
            return await _context.TaskShares
                .Include(ts => ts.Task)
                .ThenInclude(t => t.Section)
                .Where(ts => ts.UserId == userId)
                .ToListAsync();
        }

        public async Task ShareTaskAsync(TaskShare share)
        {
            _context.TaskShares.Add(share);
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsTaskTitleExistsAsync(int sectionId, string title)
        {
            return await _context.Tasks
                .AnyAsync(t => t.SectionId == sectionId && t.Title == title);
        }

        public async Task<bool> IsTaskTitleExistsInOtherTasksAsync(int sectionId, string title, int taskId)
        {
            return await _context.Tasks
                .AnyAsync(t => t.SectionId == sectionId && t.Title == title && t.Id != taskId);
        }

        public async Task<bool> IsTaskAlreadySharedAsync(int taskId, int userId)
        {
            return await _context.TaskShares
                .AnyAsync(ts => ts.TaskId == taskId && ts.UserId == userId);
        }
    }
}