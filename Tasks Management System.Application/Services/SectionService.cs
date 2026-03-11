using AutoMapper;
using Tasks_Management_System.Application.Common;
using Tasks_Management_System.Application.DTOs.Section;
using Tasks_Management_System.Application.Interfaces.Sections;
using Tasks_Management_System.Application.InterfacesServices;
using Tasks_Management_System.Application.Validators;
using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Infrastructure.Services
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _repository;
        private readonly IMapper _mapper;
        private readonly SectionValidator _validator;

        public SectionService(ISectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = new SectionValidator(repository);
        }

        // Create Section
        public async Task<ApiResponse<SectionResponseDto>> CreateSectionAsync(int userId, CreateSectionDto dto)
        {
            var nameError = await _validator.ValidateSectionNameAsync(userId, dto.Name);
            if (nameError != null)
                return new ApiResponse<SectionResponseDto> { Success = false, Message = nameError, Data = null };

            var section = _mapper.Map<Section>(dto);
            section.OwnerId = userId;

            var createdSection = await _repository.CreateSectionAsync(section);

            return new ApiResponse<SectionResponseDto>
            {
                Success = true,
                Message = "Section created successfully",
                Data = _mapper.Map<SectionResponseDto>(createdSection)
            };
        }

        // Update Section
        public async Task<ApiResponse<SectionResponseDto>> UpdateSectionAsync(int userId, UpdateSectionDto dto)
        {
            var section = await _repository.GetSectionByIdAsync(dto.Id);
            var nameError = await _validator.ValidateSectionNameAsync(userId, dto.Name);
            if (nameError != null)
                return new ApiResponse<SectionResponseDto> { Success = false, Message = nameError, Data = null };

            var ownerError = _validator.ValidateOwner(userId, section);
            if (ownerError != null)
                return new ApiResponse<SectionResponseDto> { Success = false, Message = ownerError, Data = null };

            section.Name = dto.Name;

            await _repository.UpdateSectionAsync(section);

            return new ApiResponse<SectionResponseDto>
            {
                Success = true,
                Message = "Section updated successfully",
                Data = _mapper.Map<SectionResponseDto>(section)
            };
        }

        // Delete Section
        public async Task<ApiResponse<string>> DeleteSectionAsync(int userId, int sectionId)
        {
            var section = await _repository.GetSectionByIdAsync(sectionId);

            var ownerError = _validator.ValidateOwner(userId, section);
            if (ownerError != null)
                return new ApiResponse<string> { Message = ownerError, Data = null };

            await _repository.DeleteSectionAsync(section);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Section deleted successfully",
                Data = "Deleted"
            };
        }

        // Get My Sections
        public async Task<ApiResponse<List<SectionResponseDto>>> GetMySectionsAsync(int userId)
        {
            var sections = await _repository.GetUserSectionsAsync(userId);

            return new ApiResponse<List<SectionResponseDto>>
            {
                Success = true,
                Message = "Sections retrieved successfully",
                Data = _mapper.Map<List<SectionResponseDto>>(sections)
            };
        }

        // Get Shared Sections
        public async Task<ApiResponse<List<SectionResponseDto>>> GetSharedSectionsAsync(int userId)
        {
            var sharedSections = await _repository.GetSharedSectionsAsync(userId);
            var result = _mapper.Map<List<SectionResponseDto>>(
                sharedSections.Select(ss => ss.Section).ToList()
            );

            return new ApiResponse<List<SectionResponseDto>>
            {
                Success = true,
                Message = "Shared sections retrieved successfully",
                Data = result
            };
        }

        // Share Section
        public async Task<ApiResponse<string>> ShareSectionAsync(int ownerId, ShareSectionDto dto)
        {
            var section = await _repository.GetSectionByIdAsync(dto.SectionId);

            var shareError = await _validator.ValidateShareAsync(section, dto.UserEmail);
            if (shareError != null)
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = shareError,
                    Data = null
                };

            var user = await _repository.GetUserByEmailAsync(dto.UserEmail);

            var sectionShare = new SectionShare
            {
                SectionId = section.Id,
                UserId = user.Id
            };

            await _repository.ShareSectionAsync(sectionShare);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Section shared successfully",
                Data = "Shared"
            };
        }
    }
}