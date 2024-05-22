namespace Mcm.Api.Displayables;

#nullable enable

/// <summary>
/// Text component
/// </summary>
public interface IText : IDisplayable
{
    string Content { get; set; }
    float? FontSize { get; }
}
