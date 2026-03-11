using System.Text.RegularExpressions;
using Tasks_Management_System.Application.Interfaces.Tasks;
using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Application.Validators
{
    public class TaskValidator
    {
        private readonly ITaskRepository _repository;

        public TaskValidator(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> ValidateTaskTitleAsync(int sectionId, string title, int? taskId = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                return "Title is required";

            if (!Regex.IsMatch(title, @"^[A-Za-z]"))
                return "Title must start with a letter";

            if (title.Length > 200)
                return "Title must be less than 200 characters";

            bool exists;
            if (taskId.HasValue)
            {
                exists = await _repository.IsTaskTitleExistsInOtherTasksAsync(sectionId, title, taskId.Value);
            }
            else
            {
                exists = await _repository.IsTaskTitleExistsAsync(sectionId, title);
            }

            if (exists)
                return "Task title already exists in this section";

            return null;
        }

        public async Task<string> ValidateTaskDescriptionAsync(int sectionId, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return "Description is required";

            if (description.Length > 1000)
                return "Description must be less than 1000 characters";

            return null;
        }

        public string ValidateOwner(int userId, TaskItem task)
        {
            if (task == null)
                return "Task not found";

            if (task.OwnerId != userId)
                return "You are not allowed to perform this action";

            return null;
        }

        public async Task<string> ValidateShareTaskAsync(int taskId, int targetUserId)
        {
            var alreadyShared = await _repository.IsTaskAlreadySharedAsync(taskId, targetUserId);

            if (alreadyShared)
                return "This task is already shared with this user";

            return null;
        }
    }
}