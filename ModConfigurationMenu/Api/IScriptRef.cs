namespace Mcm.Api;

#nullable enable

/// <summary>
/// Holds a ref to script-created game object<br/>
/// Responsible for its destruction
/// </summary>
internal interface IScriptRef
{
    GameObject? Ref { get; }
}
