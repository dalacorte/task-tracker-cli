using System.Runtime.InteropServices;

namespace TaskTracker.CLI;

public static class Globals
{
    public static readonly string TASK_FILE_NAME = "tasks.json";

    public static readonly string TASK_FILE_LOCATION =
        Path.Combine(GetBaseDirectory(), TASK_FILE_NAME);

    private static string GetBaseDirectory()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
}