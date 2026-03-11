using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks_Management_System.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Section> Sections { get; set; } = new List<Section>();

        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();

        public ICollection<TaskShare> SharedTasks { get; set; } = new List<TaskShare>();

        public ICollection<SectionShare> SharedSections { get; set; } = new List<SectionShare>();
    }
}