using TaskTracker.CLI.Commands.Helpers;
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

        List<Task> tasks = FileHelper.LoadTasks();
        int nextId = FileHelper.GetNextId(tasks);

        Task newTask = new(description)
        {
            Id = nextId
        };

        tasks.Add(newTask);
        FileHelper.SaveTasks(tasks);

        return newTask;
    }
}