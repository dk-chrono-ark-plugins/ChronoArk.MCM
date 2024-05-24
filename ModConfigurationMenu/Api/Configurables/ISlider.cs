namespace Mcm.Api.Configurables;

public interface ISlider : IConfigurable<float>
{
    /// <summary>
    ///     Minimum constraint
    /// </summary>
    float Min { get; }

    /// <summary>
    ///     Maximum constraint
    /// </summary>
    float Max { get; }

    /// <summary>
    ///     Minimum step value
    /// </summary>
    float Step { get; }
}