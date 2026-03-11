using System.ComponentModel.DataAnnotations;

namespace Tasks_Management_System.Application.DTOs.Section
{
    public class ShareSectionDto
    {
        public int SectionId { get; set; }

        public string UserEmail { get; set; }
    }
}