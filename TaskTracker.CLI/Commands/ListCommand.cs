using TaskTracker.CLI.Commands.Helpers;
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
        List<Task> tasks = FileHelper.LoadTasks();

        if (status is not null)
            tasks = tasks.Where(x => x.Status == status).ToList();

        return tasks;
    }
}