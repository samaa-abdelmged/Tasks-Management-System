using AutoMapper;
using Tasks_Management_System.Application.DTOs.Task;
using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Application.Common.Mapping
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            // Create & Update DTOs → Entity
            CreateMap<CreateTaskDto, TaskItem>();
            CreateMap<UpdateTaskDto, TaskItem>();
            CreateMap<ShareTaskDto, TaskShare>();

            // Entity → Response DTO
            CreateMap<TaskItem, TaskResponseDto>()
                .ForMember(dest => dest.SectionName,
                           opt => opt.MapFrom(src => src.Section.Name));

            CreateMap<TaskShare, TaskResponseDto>()
    .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Task.Section.Name));
        }
    }
}