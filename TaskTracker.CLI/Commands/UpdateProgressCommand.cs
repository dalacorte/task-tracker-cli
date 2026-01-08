using System.Text.Json;
using TaskTracker.CLI.Entities;
using Task = TaskTracker.CLI.Entities.Task;

namespace TaskTracker.CLI.Commands;

public class UpdateProgressCommand
{
    public static int Execute(StatusEnum status, string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: id is empty.");
            Console.WriteLine("Usage: task-cli update <id> <description>");
            return 1;
        }

        string idString = args[0];

        bool success = int.TryParse(idString, out int id);

        if (!success)
            throw new ArgumentException("Id is not a number", nameof(Execute));

        Task task = Process(id, status);

        Console.WriteLine($"Task marked as {status} successfully (Id: {task.Id})");

        return 0;
    }

    private static Task Process(int id, StatusEnum status)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid id", nameof(Process));

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

        task.Status = status;
        task.UpdatedAt = DateTime.Now;

        string outputJson = JsonSerializer.Serialize(
            tasks,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(Globals.TASK_FILE_LOCATION, outputJson);

        return task;
    }
}