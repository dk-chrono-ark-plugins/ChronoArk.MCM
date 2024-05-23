namespace Mcm.Api.Displayables;

public interface ILine : IDisplayable
{
    float Thickness { get; }
    Color Color { get; }
}
