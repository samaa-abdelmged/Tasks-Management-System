using System.ComponentModel.DataAnnotations;

namespace Tasks_Management_System.Domain.Entities
{
    public class SectionShare
    {
        public int Id { get; set; }

        public int SectionId { get; set; }

        public Section Section { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}