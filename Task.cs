using System;

namespace StrukturData
{
    public class Task : IComparable<Task>
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Priority { get; set; }
        public bool IsCompleted { get; set; }

        public Task(string name, int priority = 2)
        {
            Name = name;
            Priority = priority;
            CreatedAt = DateTime.Now;
            IsCompleted = false;
        }

        public int CompareTo(Task other)
        {
            if (other == null) return 1;
            
            int priorityComparison = Priority.CompareTo(other.Priority);
            if (priorityComparison != 0) return priorityComparison;
            
            return CreatedAt.CompareTo(other.CreatedAt);
        }

        public override string ToString()
        {
            string priorityStr = Priority switch
            {
                1 => "HIGH",
                2 => "MEDIUM",
                3 => "LOW",
                _ => "UNKNOWN"
            };
            
            string status = IsCompleted ? "✓" : "○";
            return $"{status} [{priorityStr}] {Name} (Created: {CreatedAt:HH:mm:ss})";
        }
    }

    public class TaskAction
    {
        public string ActionType { get; set; } // "ADD", "REMOVE", "COMPLETE", "UPDATE"
        public Task Task { get; set; }
        public DateTime Timestamp { get; set; }

        public TaskAction(string actionType, Task task)
        {
            ActionType = actionType;
            Task = task;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{ActionType}: {Task.Name} at {Timestamp:HH:mm:ss}";
        }
    }
}