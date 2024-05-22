namespace Mcm.Api;

#nullable enable

/// <summary>
/// Holds a ref to script-created game object<br/>
/// Responsible for its destruction
/// </summary>
public interface IScriptRef
{
    /// <summary>
    /// Ref needs updating
    /// </summary>
    bool Dirty { get; }

    /// <summary>
    /// Ref to itself
    /// </summary>
    GameObject? Ref { get; }

    /// <summary>
    /// Destroy self
    /// </summary>
    void Destroy();
}
