using AutoMapper;
using Tasks_Management_System.Application.DTOs.Section;
using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Application.Mappings
{
    public class SectionMappingProfile : Profile
    {
        public SectionMappingProfile()
        {
            // Create & Update DTOs → Entity
            CreateMap<CreateSectionDto, Section>();
            CreateMap<UpdateSectionDto, Section>();

            // Entity → Response DTO
            CreateMap<Section, SectionResponseDto>();
        }
    }
}