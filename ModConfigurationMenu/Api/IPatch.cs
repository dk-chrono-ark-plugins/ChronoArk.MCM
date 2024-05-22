using Mcm.Api.Configurables;

namespace Mcm.Api;

/// <summary>
/// IPatch is a irreversible config, commit on enable
/// </summary>
public interface IPatch : IBasicEntry
{
    /// <summary>
    /// The patch itself
    /// </summary>
    void Commit();
}
