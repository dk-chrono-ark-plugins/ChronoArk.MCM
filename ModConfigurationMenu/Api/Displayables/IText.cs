using TMPro;

namespace Mcm.Api.Displayables;

/// <summary>
///     Text component
/// </summary>
public interface IText : IStylable
{
    TextMeshProUGUI? Text { get; }
    string Content { get; set; }
}