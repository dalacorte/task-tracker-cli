using System.Runtime.InteropServices;

namespace TaskTracker.CLI;

public static class Globals
{
    public static readonly string TASK_FILE_NAME = "tasks.json";

    public static readonly string TASK_FILE_LOCATION =
        Path.Combine(GetBaseDirectory(), TASK_FILE_NAME);

    private static string GetBaseDirectory()
    {
        string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "task-tracker-cli"
        );

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        return path;
    }
}