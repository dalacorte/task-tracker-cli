using TaskTracker.CLI.Commands.Helpers;
using Task = TaskTracker.CLI.Entities.Task;

namespace TaskTracker.CLI.Commands;

public class DeleteCommand
{
    public static int Execute(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: id is empty.");
            Console.WriteLine("Usage: task-cli delete <id>");
            return 1;
        }

        string idString = args[0];

        bool success = int.TryParse(idString, out int id);

        if (!success)
            throw new ArgumentException("Id is not a number", nameof(Execute));

        Process(id);

        Console.WriteLine($"Task deleted successfully (Id: {id})");

        return 0;
    }

    private static Task Process(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid id", nameof(Process));

        List<Task> tasks = FileHelper.LoadTasks();
        Task? task = FileHelper.FindTaskById(tasks, id);

        if (task is null)
            throw new ArgumentException("Task not found", nameof(Process));

        tasks.Remove(task);
        FileHelper.SaveTasks(tasks);

        return task;
    }
}