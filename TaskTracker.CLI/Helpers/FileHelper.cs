using System.Text.Json;
using Task = TaskTracker.CLI.Entities.Task;

namespace TaskTracker.CLI.Commands.Helpers;

public static class FileHelper
{
    public static List<Task> LoadTasks()
    {
        if (File.Exists(Globals.TASK_FILE_LOCATION))
        {
            string json = File.ReadAllText(Globals.TASK_FILE_LOCATION);

            return string.IsNullOrWhiteSpace(json)
                ? new List<Task>()
                : JsonSerializer.Deserialize<List<Task>>(json)
                  ?? new List<Task>();
        }
        
        return new List<Task>();
    }

    public static void SaveTasks(List<Task> tasks)
    {
        string outputJson = JsonSerializer.Serialize(
            tasks,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(Globals.TASK_FILE_LOCATION, outputJson);
    }

    public static Task? FindTaskById(List<Task> tasks, int id)
    {
        return tasks.FirstOrDefault(t => t.Id == id);
    }

    public static int GetNextId(List<Task> tasks)
    {
        return tasks.Count == 0
            ? 1
            : tasks.Max(t => t.Id) + 1;
    }
}