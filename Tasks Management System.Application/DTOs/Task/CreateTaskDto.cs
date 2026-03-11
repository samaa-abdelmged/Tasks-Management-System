namespace Tasks_Management_System.Application.DTOs.Task
{
    public class CreateTaskDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int SectionId { get; set; }
    }
}