namespace Mcm.Implementation.Displayables;

#nullable enable

internal partial class McmPage : McmStylable, IPage
{
    public IImage AddImage(Color color)
    {
        var mcmImage = new McmImage() {
            MaskColor = color,
        };
        Add(mcmImage);
        return mcmImage;
    }

    public IImage AddImage(Sprite sprite)
    {
        var mcmImage = new McmImage() {
            MainSprite = sprite,
        };
        Add(mcmImage);
        return mcmImage;
    }

    public IImage AddImage(string assetName)
    {
        var texture = Owner.LoadTexture2D(assetName);
        var mcmImage = new McmImage() {
            MainSprite = Misc.CreatSprite(texture),
            Size = new(texture.width, texture.height),
        };
        Add(mcmImage);
        return mcmImage;
    }

    public ILine AddSeparator(float thickness = 5f, Color? color = null)
    {
        var mcmLine = new McmSeparator() {
            Color = color ?? Color.gray,
            Thickness = thickness,
        };
        Add(mcmLine);
        return mcmLine;
    }

    public IText AddText(string text)
    {
        var textLoc = Owner.I2Loc(text);
        var mcmText = new McmText() {
            Content = textLoc,
            FontSize = 30f,
        };
        Add(mcmText);
        return mcmText;
    }
}
