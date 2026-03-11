using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Tasks_Management_System.Domain.Entities
{
    [Index(nameof(Title), IsUnique = true)]
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public int SectionId { get; set; }

        public Section Section { get; set; }

        public int OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public ICollection<TaskShare> SharedWithUsers { get; set; } = new List<TaskShare>();
    }
}