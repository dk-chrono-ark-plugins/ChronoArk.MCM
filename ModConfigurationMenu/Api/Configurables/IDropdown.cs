namespace Mcm.Api.Configurables;

public interface IDropdown : IConfigurable<int>
{
    /// <summary>
    ///     List of options
    /// </summary>
    string[] Options { get; }
}