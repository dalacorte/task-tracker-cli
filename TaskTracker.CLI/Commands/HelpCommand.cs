namespace TaskTracker.CLI.Commands;

public class HelpCommand
{
    public static int Execute()
    {
        Print();
        return 0;
    }

    public static void Print()
    {
        Console.WriteLine("""
                          Usage:
                            task-cli <command> [args]

                          Commands:
                            add <description>            Adds a new task
                            update <id> <description>    Updates a new task
                            delete <id> <description>    Deletes a new task
                            mark-in-progress <id>        Marks the task as in progress
                            mark-done <id>               Marks the task as done
                            list                         Lists all tasks
                            list done                    Lists all tasks marked as done
                            list todo                    Lists all tasks marked as todo
                            list in-progress             Lists all tasks marked as in progress
                            help                         Show's this help

                          Options:
                            -h, --help                   Show's this help
                          """);
    }
}