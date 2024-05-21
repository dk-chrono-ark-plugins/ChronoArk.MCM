namespace Mcm.Implementation;

#nullable enable

internal static class ModLog
{
    internal static void Log(this string message)
    {
        Debug.Log($"[MCM] {message}");
    }
}
