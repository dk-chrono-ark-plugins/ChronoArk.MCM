using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation.Configurables;

internal class McmToggle : McmConfigurable<bool>, IToggle
{
    private readonly McmComposite _configurable;
    private readonly McmImage _off;
    private readonly McmImage _on;

    public McmToggle(string key, McmSettingEntry entry)
        : base(key, entry.Name, entry.Description, McmStyle.Default())
    {
        Style.OutlineSize = null;

        var stateStyle = Style with {
            Size = McmStyle.SettingLayout.ToggleSingle,
            TextFontSize = 50f,
        };

        _on = new(stateStyle);
        _off = new(stateStyle);

        var on = new McmLayerText(_on, stateStyle) {
            Content = McmLoc.Setting.ToggleOn,
        };
        var off = new McmLayerText(_off, stateStyle) {
            Content = McmLoc.Setting.ToggleOff,
        };

        var toggleStyle = Style with {
            Size = McmStyle.SettingLayout.Setting,
            LayoutPadding = McmStyle.SettingLayout.TogglePadding,
            LayoutSpacing = McmStyle.SettingLayout.ToggleSpacing,
        };

        var toggle = new McmButton(toggleStyle) {
            Content = new McmHorizontal(toggleStyle) {
                Composites = [
                    new(on, McmStyle.SettingLayout.ToggleSingle),
                    new(off, McmStyle.SettingLayout.ToggleSingle),
                ],
            },
            OnClick = () => SetValue(!Value),
            DisableGradient = true,
        };

        var configurableStyle = Style with {
            LayoutSpacing = McmStyle.SettingLayout.SettingSpacingInner,
        };
        _configurable = new McmHorizontal(configurableStyle) {
            Composites = [
                .. _entry,
                new(toggle, toggleStyle.Size.Value),
            ],
        };
    }

    public override IBasicEntry.EntryType SettingType => IBasicEntry.EntryType.Toggle;

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var toggle = _configurable.Render(parent);
        Value = Read();

        return base.Render(toggle);
    }

    public override void Update()
    {
        _on.Style = _on.Style with { OutlineSize = Value ? new(5f, 5f) : Vector2.zero };
        _off.Style = _off.Style with { OutlineSize = !Value ? new(5f, 5f) : Vector2.zero };
    }
}