namespace Tasks_Management_System.Application.DTOs.Task
{
    public class TaskResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int SectionId { get; set; }

        public string SectionName { get; set; }
    }
}