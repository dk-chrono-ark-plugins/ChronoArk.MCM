﻿namespace Mcm.Implementation.Displayables;

internal partial class McmPage
{
    public IImage AddImage(Color color)
    {
        var mcmImage = new McmImage {
            Style = {
                ColorPrimary = color,
            },
        };
        Add(mcmImage);
        return mcmImage;
    }

    public IImage AddImage(Sprite sprite)
    {
        var mcmImage = new McmImage {
            MainSprite = sprite,
        };
        Add(mcmImage);
        return mcmImage;
    }

    public IImage AddImage(string assetName)
    {
        var texture = Owner.LoadTexture2D(assetName);
        var mcmImage = new McmImage {
            MainSprite = Misc.CreatSprite(texture),
        };
        Add(mcmImage);
        return mcmImage;
    }

    public ILine AddSeparator(float thickness = 5f, Color? color = null)
    {
        var mcmLine = new McmSeparator {
            Style = {
                ColorSecondary = color,
                OutlineSize = new(0f, thickness),
            },
        };
        Add(mcmLine);
        return mcmLine;
    }

    public IText AddText(string text)
    {
        var textLoc = Owner.I2Loc(text);
        var mcmText = new McmText {
            Content = textLoc,
        };
        Add(mcmText);
        return mcmText;
    }
}