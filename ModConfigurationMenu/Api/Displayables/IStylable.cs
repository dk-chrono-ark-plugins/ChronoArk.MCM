namespace Mcm.Api.Displayables;

/// <summary>
///     Allows editing styles
/// </summary>
public interface IStylable : IDisplayable
{
    /// <summary>
    ///     Style holder
    /// </summary>
    McmStyle Style { get; set; }
}