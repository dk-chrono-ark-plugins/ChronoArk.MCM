namespace Mcm.Api.Configurables;

public interface IBasicEntry
{
    enum EntryType
    {
        Unknown,

        // CARK supported
        Dropdown,
        Input,
        InputDecimal,
        InputInteger,
        Slider,
        Toggle,

        // Mcm added
        FileBrowser,
    }

    /// <summary>
    ///     The unique ID of this configuration
    /// </summary>
    string Id { get; }

    /// <summary>
    ///     The name entry in config menu
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     The explanatory info for this entry
    /// </summary>
    string Description { get; }

    /// <summary>
    ///     Self explanatory
    /// </summary>
    EntryType SettingType { get; }
}