using TMPro;

namespace Mcm.Api.Displayables;

#nullable enable

/// <summary>
/// Text component
/// </summary>
public interface IText : IDisplayable
{
    TextMeshProUGUI? Text { get; }
    string Content { get; set; }
    float? FontSize { get; }
}
