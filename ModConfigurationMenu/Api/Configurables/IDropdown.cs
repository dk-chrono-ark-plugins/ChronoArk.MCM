namespace Mcm.Api.Configurables;

#nullable enable

public interface IDropdown : IConfigurable<int>
{
    /// <summary>
    /// List of options
    /// </summary>
    IDisplayable[] Options { get; }
}
