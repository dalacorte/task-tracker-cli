using TaskTracker.CLI;
using TaskTracker.CLI.Commands;

if (!File.Exists(Globals.TASK_FILE_LOCATION))
{
    File.WriteAllText(Globals.TASK_FILE_LOCATION, "[]");
}

if (args.Length == 0)
{
    HelpCommand.Print();
    return 1;
}

return CommandDispatcher.Dispatch(args);