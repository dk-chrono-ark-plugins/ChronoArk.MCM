namespace Mcm.Api.Configurables;

public interface IDropdown : IConfigurable<int>
{
    List<IDropdownEntry> Options { get; }

    int CurrentOption { get; }
}
