using System.ComponentModel.DataAnnotations;

namespace Tasks_Management_System.Domain.Entities
{
    public class TaskShare
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public TaskItem Task { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}