using System.Text.RegularExpressions;
using Tasks_Management_System.Application.Interfaces.Sections;
using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Application.Validators
{
    public class SectionValidator
    {
        private readonly ISectionRepository _repository;

        public SectionValidator(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> ValidateSectionNameAsync(int userId, string name, int? sectionIdToIgnore = null)
        {
            // Required
            if (string.IsNullOrWhiteSpace(name))
                return "Section name is required";

            // Max length 100
            if (name.Length > 100)
                return "Section name must not exceed 100 characters";

            // Must start with a letter
            if (!Regex.IsMatch(name, @"^[A-Za-z]"))
                return "Section name must start with a letter";

            // Only letters, numbers, spaces
            if (!Regex.IsMatch(name, @"^[A-Za-z][A-Za-z0-9\s]*$"))
                return "Section name can contain only letters, numbers, and spaces";

            // Unique for this user
            var existingSection = await _repository.GetSectionByNameAsync(userId, name);
            if (existingSection != null && existingSection.Id != sectionIdToIgnore)
                return "Section name already exists";

            return null;
        }

        // Validate section ownership
        public string ValidateOwner(int userId, Section section)
        {
            if (section.OwnerId != userId)
                return "Unauthorized";
            return null;
        }

        // Validate sharing
        public async Task<string> ValidateShareAsync(Section section, string userEmail)
        {
            if (section == null)
                return "Section Not Found";

            var user = await _repository.GetUserByEmailAsync(userEmail);
            if (user == null)
                return "User Not Found";

            var alreadyShared = await _repository.IsSectionSharedWithUser(section.Id, user.Id);
            if (alreadyShared)
                return "Section already shared with this user";

            return null;
        }
    }
}