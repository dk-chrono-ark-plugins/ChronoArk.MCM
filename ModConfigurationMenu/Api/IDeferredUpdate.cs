namespace Mcm.Api;

#nullable enable

/// <summary>
/// Defer update/state sync to next condition check
/// </summary>
public interface IDeferredUpdate
{
    /// <summary>
    /// Is currently having deferred awaiter
    /// </summary>
    bool Deferred { get; }

    /// <summary>
    /// Delegate a deferred update or merge existing
    /// </summary>
    void DeferredUpdate();

    /// <summary>
    /// Main update method that gets deferred
    /// </summary>
    void Update();
}
