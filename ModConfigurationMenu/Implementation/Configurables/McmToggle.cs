using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation.Configurables;

internal class McmToggle : McmConfigurable<bool>, IToggle
{
    private readonly McmImage _off;
    private readonly McmImage _on;
    private readonly McmComposite _toggle;

    public McmToggle(string key, McmSettingEntry entry)
        : base(key, entry.Name, entry.Description, McmStyle.Default())
    {
        Style.Size = McmStyle.SettingLayout.ToggleSingle;
        Style.TextFontSize = 50f;
        Style.OutlineSize = null;

        _on = new(Style);
        _off = new(Style);
        var on = new McmComposite(ICompositeLayout.LayoutGroup.Overlap, Style) {
            Composites = [
                new(_on, McmStyle.SettingLayout.ToggleSingle),
                new(new McmText(Style) {
                        Content = McmLoc.Setting.ToggleOn,
                    },
                    McmStyle.SettingLayout.ToggleSingle),
            ],
        };
        var off = new McmComposite(ICompositeLayout.LayoutGroup.Overlap, Style) {
            Composites = [
                new(_off,
                    McmStyle.SettingLayout.ToggleSingle),
                new(new McmText(Style) {
                        Content = McmLoc.Setting.ToggleOff,
                    },
                    McmStyle.SettingLayout.ToggleSingle),
            ],
        };

        Style.Size = McmStyle.SettingLayout.Setting;
        Style.LayoutPadding = McmStyle.SettingLayout.TogglePadding;
        Style.LayoutSpacing = McmStyle.SettingLayout.ToggleSpacing;

        var toggle = new McmButton(Style) {
            Content = new McmComposite(ICompositeLayout.LayoutGroup.Horizontal, Style) {
                Composites = [
                    new(on, McmStyle.SettingLayout.ToggleSingle),
                    new(off, McmStyle.SettingLayout.ToggleSingle),
                ],
            },
            OnClick = () => SetValue(!Value),
            DisableGradient = true,
        };

        Style.LayoutSpacing = McmStyle.SettingLayout.SettingSpacingInner;
        _toggle = new(ICompositeLayout.LayoutGroup.Horizontal, Style) {
            Composites = [
                .. _entry,
                new(toggle, Style.Size!.Value),
            ],
        };
    }

    public override IBasicEntry.EntryType SettingType => IBasicEntry.EntryType.Toggle;

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var toggle = _toggle.Render(parent);
        Value = Read();

        return base.Render(toggle);
    }

    public override void Update()
    {
        _on.Style = _on.Style with { OutlineSize = Value ? new(5f, 5f) : Vector2.zero };
        _off.Style = _off.Style with { OutlineSize = !Value ? new(5f, 5f) : Vector2.zero };
    }
}