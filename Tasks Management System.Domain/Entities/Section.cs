using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks_Management_System.Domain.Entities
{
    public class Section
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public ICollection<SectionShare> SharedWithUsers { get; set; } = new List<SectionShare>();
    }
}