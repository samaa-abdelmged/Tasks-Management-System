using AutoMapper;
using Tasks_Management_System.Application.Common;
using Tasks_Management_System.Application.DTOs.Task;
using Tasks_Management_System.Application.Interfaces.Tasks;
using Tasks_Management_System.Application.InterfacesServices;
using Tasks_Management_System.Application.Validators;
using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;
        private readonly TaskValidator _validator;

        public TaskService(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = new TaskValidator(repository);
        }

        public async Task<ApiResponse<TaskResponseDto>> CreateTaskAsync(int userId, CreateTaskDto dto)
        {
            var titleError = await _validator.ValidateTaskTitleAsync(dto.SectionId, dto.Title);
            if (titleError != null)
                return new ApiResponse<TaskResponseDto> { Success = false, Message = titleError };

            var descriptionError = await _validator.ValidateTaskDescriptionAsync(dto.SectionId, dto.Description);
            if (descriptionError != null)
                return new ApiResponse<TaskResponseDto> { Success = false, Message = descriptionError };

            var task = _mapper.Map<TaskItem>(dto);
            task.OwnerId = userId;

            var createdTask = await _repository.CreateTaskAsync(task);

            return new ApiResponse<TaskResponseDto>
            {
                Success = true,
                Message = "Task created successfully",
                Data = _mapper.Map<TaskResponseDto>(createdTask)
            };
        }

        public async Task<ApiResponse<TaskResponseDto>> UpdateTaskAsync(int userId, UpdateTaskDto dto)
        {
            var task = await _repository.GetTaskByIdAsync(dto.Id);
            var ownerError = _validator.ValidateOwner(userId, task);
            if (ownerError != null)
                return new ApiResponse<TaskResponseDto> { Success = false, Message = ownerError };

            var titleError = await _validator.ValidateTaskTitleAsync(task.SectionId, dto.Title, task.Id);
            if (titleError != null)
                return new ApiResponse<TaskResponseDto> { Success = false, Message = titleError };

            var descriptionError = await _validator.ValidateTaskDescriptionAsync(task.SectionId, dto.Description);
            if (descriptionError != null)
                return new ApiResponse<TaskResponseDto> { Success = false, Message = descriptionError };

            task.Title = dto.Title;
            task.Description = dto.Description;

            await _repository.UpdateTaskAsync(task);

            return new ApiResponse<TaskResponseDto>
            {
                Success = true,
                Message = "Task updated successfully",
                Data = _mapper.Map<TaskResponseDto>(task)
            };
        }

        public async Task<ApiResponse<string>> DeleteTaskAsync(int userId, int taskId)
        {
            var task = await _repository.GetTaskByIdAsync(taskId);
            var ownerError = _validator.ValidateOwner(userId, task);
            if (ownerError != null)
                return new ApiResponse<string> { Success = false, Message = ownerError };

            await _repository.DeleteTaskAsync(task);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Task deleted successfully",
                Data = "Deleted"
            };
        }

        public async Task<ApiResponse<List<TaskResponseDto>>> GetMyTasksAsync(int userId)
        {
            var tasks = await _repository.GetUserTasksAsync(userId);
            return new ApiResponse<List<TaskResponseDto>>
            {
                Success = true,
                Message = "Tasks retrieved successfully",
                Data = _mapper.Map<List<TaskResponseDto>>(tasks)
            };
        }

        public async Task<ApiResponse<List<TaskResponseDto>>> GetSectionTasksAsync(int sectionId)
        {
            var tasks = await _repository.GetSectionTasksAsync(sectionId);
            return new ApiResponse<List<TaskResponseDto>>
            {
                Success = true,
                Message = "Section tasks retrieved successfully",
                Data = _mapper.Map<List<TaskResponseDto>>(tasks)
            };
        }

        public async Task<ApiResponse<List<TaskResponseDto>>> GetSharedTasksAsync(int userId)
        {
            var shared = await _repository.GetSharedTasksAsync(userId);
            var result = _mapper.Map<List<TaskResponseDto>>(shared.Select(s => s.Task).ToList());

            return new ApiResponse<List<TaskResponseDto>>
            {
                Success = true,
                Message = "Shared tasks retrieved successfully",
                Data = result
            };
        }

        public async Task<ApiResponse<string>> ShareTaskAsync(int ownerId, ShareTaskDto dto)
        {
            var task = await _repository.GetTaskByIdAsync(dto.TaskId);
            if (task == null)
                return new ApiResponse<string> { Success = false, Message = "Task not found" };

            var ownerError = _validator.ValidateOwner(ownerId, task);
            if (ownerError != null)
                return new ApiResponse<string> { Success = false, Message = ownerError };

            var user = await _repository.GetUserByEmailAsync(dto.UserEmail);
            if (user == null)
                return new ApiResponse<string> { Success = false, Message = "User to share with not found" };

            var shareError = await _validator.ValidateShareTaskAsync(task.Id, user.Id);
            if (shareError != null)
                return new ApiResponse<string> { Success = false, Message = shareError };

            var share = new TaskShare
            {
                TaskId = task.Id,
                UserId = user.Id
            };

            await _repository.ShareTaskAsync(share);

            return new ApiResponse<string> { Success = true, Message = "Task shared successfully", Data = "Shared" };
        }
    }
}