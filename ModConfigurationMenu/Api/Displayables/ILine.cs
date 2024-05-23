namespace Mcm.Api.Displayables;

#nullable enable

public interface ILine : IDisplayable
{
    float Thickness { get; }
    Color Color { get; }
}
