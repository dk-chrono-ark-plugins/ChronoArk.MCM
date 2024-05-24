using UnityEngine.UI;

namespace Mcm.Api.Displayables;

#nullable enable

/// <summary>
/// Image component
/// </summary>
public interface IImage : IStylable
{
    public Sprite? MainSprite { get; }
    public Image? Image { get; }
}
