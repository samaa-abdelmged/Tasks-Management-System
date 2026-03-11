using System.ComponentModel.DataAnnotations;

namespace Tasks_Management_System.Application.DTOs.Section
{
    public class UpdateSectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}