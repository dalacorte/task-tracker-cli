using System.Text.Json;
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

        List<Task> tasks;

        if (File.Exists(Globals.TASK_FILE_LOCATION))
        {
            string json = File.ReadAllText(Globals.TASK_FILE_LOCATION);

            tasks = string.IsNullOrWhiteSpace(json)
                ? new List<Task>()
                : JsonSerializer.Deserialize<List<Task>>(json)
                  ?? new List<Task>();
        }
        else
        {
            tasks = new List<Task>();
        }

        Task? task = tasks.FirstOrDefault(t => t.Id == id);

        if (task is null)
            throw new ArgumentException("Task not found", nameof(Process));

        task.Description = description;
        task.UpdatedAt = DateTime.Now;

        string outputJson = JsonSerializer.Serialize(
            tasks,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(Globals.TASK_FILE_LOCATION, outputJson);

        return task;
    }
}