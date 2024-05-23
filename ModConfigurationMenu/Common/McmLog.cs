namespace Mcm.Common;

#nullable enable

internal static class Debug
{
    internal static void Log(object message)
    {
        UnityEngine.Debug.Log($"[MCM] {message}");
    }
}
