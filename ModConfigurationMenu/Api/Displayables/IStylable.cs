namespace Mcm.Api.Displayables;

#nullable enable

/// <summary>
/// Allows editing styles
/// </summary>
public interface IStylable : IDisplayable
{
    /// <summary>
    /// Style holder
    /// </summary>
    McmStyle Style { get; set; }
}
