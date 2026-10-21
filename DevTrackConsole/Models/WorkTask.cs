using DevTrackConsole.Enums;

namespace DevTrackConsole.Models
{
    public class WorkTask 
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskPriority Priority { get; set; }
        public WorkTaskStatus Status { get; set; } = WorkTaskStatus.ToDo;
    }
}
