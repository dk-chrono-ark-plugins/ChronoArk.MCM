namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmStylable(McmStyle? Style = null) : McmDisplayable, IStylable
{
    public McmStyle McmStyle { get; set; } = Style ?? McmStyle.Default;
}
