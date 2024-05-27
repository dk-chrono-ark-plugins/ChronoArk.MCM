namespace Mcm.Implementation.Displayables;

public partial class McmPage
{
    public IButton AddButton(string content, Action onClick)
    {
        var btnStyle = McmStyle.Default() with {
            Size = McmStyle.SettingLayout.ToggleSingle,
            TextFontSize = 44f,
            LayoutPadding = McmStyle.SettingLayout.InputPadding,
            LayoutSpacing = Vector2.zero,
            OutlineSize = new(3f, 3f),
        };
        var text = new McmText(btnStyle) {
            Content = content,
        };
        return AddButton(text, onClick);
    }

    public IButton AddButton(IDisplayable content, Action onClick)
    {
        var btnStyle = McmStyle.Default() with {
            Size = McmStyle.SettingLayout.ToggleSingle,
            TextFontSize = 44f,
            LayoutPadding = McmStyle.SettingLayout.InputPadding,
            LayoutSpacing = Vector2.zero,
            OutlineSize = new(3f, 3f),
        };
        var mcmButton = new McmButton(btnStyle) {
            Content = content,
            OnClick = onClick,
        };
        var btnHolder = new McmHorizontal(btnStyle) {
            Composites = [
                new(mcmButton, btnStyle.Size.Value),
            ],
        };
        Add(btnHolder);
        return mcmButton;
    }

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

    public IText AddTextLoc(string locKey)
    {
        return AddText(Owner.I2Loc(locKey));
    }
}