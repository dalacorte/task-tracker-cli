using TaskTracker.CLI.Commands.Helpers;
using Task = TaskTracker.CLI.Entities.Task;

namespace TaskTracker.CLI.Commands;

public class UpdateCommand
{
    public static int Execute(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Error: missing arguments.");
            Console.WriteLine("Usage: task-cli update <id> <description>");
            return 1;
        }

        string idString = args[0];
        string description = args[1];

        bool success = int.TryParse(idString, out int id);

        if (!success)
            throw new ArgumentException("Id is not a number", nameof(Execute));

        Task task = Process(id, description);

        Console.WriteLine($"Task updated successfully (Id: {task.Id}, Description: {task.Description})");

        return 0;
    }

    private static Task Process(int id, string description)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid id", nameof(Process));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty", nameof(Process));

        List<Task> tasks = FileHelper.LoadTasks();
        Task? task = FileHelper.FindTaskById(tasks, id);

        if (task is null)
            throw new ArgumentException("Task not found", nameof(Process));

        task.Description = description;
        task.UpdatedAt = DateTime.Now;

        FileHelper.SaveTasks(tasks);

        return task;
    }
}