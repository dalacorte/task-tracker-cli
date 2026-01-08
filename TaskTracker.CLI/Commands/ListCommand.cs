using System.Text.Json;
using TaskTracker.CLI.Entities;
using Task = TaskTracker.CLI.Entities.Task;

namespace TaskTracker.CLI.Commands;

public class ListCommand
{
    public static int Execute(string[] args)
    {
        StatusEnum? status = null;

        if (args.Length > 0)
        {
            status = args[0] switch
            {
                "done" => StatusEnum.Done,
                "todo" => StatusEnum.Todo,
                "in-progress" => StatusEnum.InProgress,
                _ => null
            };
        }

        List<Task> tasks = Process(status);

        foreach (Task task in tasks)
            Console.WriteLine(
                $"Id: {task.Id}, Description: {task.Description}, Status: {task.Status}, CreatedAt:  {task.CreatedAt}, UpdatedAt: {task.UpdatedAt}");

        return 0;
    }

    private static List<Task> Process(StatusEnum? status = null)
    {
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

        if (status is not null)
            tasks = tasks.Where(x => x.Status == status).ToList();

        return tasks;
    }
}