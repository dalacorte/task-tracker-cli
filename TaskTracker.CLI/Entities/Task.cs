namespace TaskTracker.CLI.Entities;

public class Task(string description)
{
    public int Id { get; set; }
    public string Description { get; set; } = description;
    public StatusEnum Status { get; set; } = StatusEnum.Todo;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}