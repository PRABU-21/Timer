
using System;

namespace TaskTimerApp.Models
{
    public class TaskItem
    {
        public Guid TaskId { get; set; }

        public Guid UserId { get; set; }

        public string TaskName { get; set; } = string.Empty;

        public TimeSpan TotalDuration { get; set; }
    }
}
