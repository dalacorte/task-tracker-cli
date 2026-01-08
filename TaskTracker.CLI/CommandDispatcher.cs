using TaskTracker.CLI.Commands;
using TaskTracker.CLI.Entities;

namespace TaskTracker.CLI;

public class CommandDispatcher
{
    public static int Dispatch(string[] args)
    {
        string command = args[0].ToLowerInvariant();
        string[] commandArgs = args.Skip(1).ToArray();

        return command switch
        {
            "add" => AddCommand.Execute(commandArgs),
            "update" => UpdateCommand.Execute(commandArgs),
            "delete" => DeleteCommand.Execute(commandArgs),
            "mark-in-progress" => UpdateProgressCommand.Execute(StatusEnum.InProgress, commandArgs),
            "mark-done" => UpdateProgressCommand.Execute(StatusEnum.Done, commandArgs),
            "list" => ListCommand.Execute(commandArgs),
            "help" => HelpCommand.Execute(),
            "--help" => HelpCommand.Execute(),
            "-h" => HelpCommand.Execute(),
            _ => UnknownCommand(command)
        };
    }

    public static int UnknownCommand(string command)
    {
        Console.Error.WriteLine($"Unknown Command: {command}");
        HelpCommand.Print();
        return 2;
    }
}