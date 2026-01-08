using System.Text.Json;
using Task = TaskTracker.CLI.Entities.Task;

namespace TaskTracker.CLI.Commands;

public class AddCommand
{
    public static int Execute(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: description is empty.");
            Console.WriteLine("Usage: task-cli add <description>");
            return 1;
        }

        string description = args[0];

        Task task = Process(description);

        Console.WriteLine($"Task added successfully (Id: {task.Id})");

        return 0;
    }

    private static Task Process(string description)
    {
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

        int nextId = tasks.Count == 0
            ? 1
            : tasks.Max(t => t.Id) + 1;

        Task newTask = new(description)
        {
            Id = nextId
        };

        tasks.Add(newTask);

        string outputJson = JsonSerializer.Serialize(
            tasks,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(Globals.TASK_FILE_LOCATION, outputJson);

        return newTask;
    }
}