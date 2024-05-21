namespace Mcm.Api;

/// <summary>
/// IPatch is a irreversible config, commit on enable
/// </summary>
internal interface IPatch : IConfigurable
{
    /// <summary>
    /// The patch itself
    /// </summary>
    public void Commit();
}
