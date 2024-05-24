using I2.Loc;
using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation.Configurables;

#nullable enable

internal class McmToggle : McmConfigurable<bool>, IToggle
{
    private readonly McmComposite _on;
    private readonly McmComposite _off;
    private readonly McmButton _toggle;

    public McmToggle(string key, McmSettingEntry entry) : base(key, entry.Name, entry.Description)
    {
        Style = McmStyle.Default();
        Style.Size = new(200f, 100f);
        Style.TextFontSize = 50f;

        _on = new McmComposite(ICompositeLayout.LayoutGroup.Overlap, Style) {
            Composites = [
                new(new McmImage(Style),
                    new(200f, 100f)),
                new(new McmText(Style) {
                        Content = LocalizationManager.GetTranslation(ScriptTerms.UI_Option.Bool_On),
                    },
                    new(200f, 100f)),
            ],
        };
        _off = new McmComposite(ICompositeLayout.LayoutGroup.Overlap, Style) {
            Composites = [
                new(new McmImage(Style),
                    new(200f, 100f)),
                new(new McmText(Style) {
                        Content = LocalizationManager.GetTranslation(ScriptTerms.UI_Option.Bool_Off),
                    },
                    new(200f, 100f)),
            ],
        };

        Style.Size = new(400f, 100f);
        Style.LayoutPadding = new(5, 5, 10, 10);
        Style.LayoutSpacing = new(5f, 0f);
        _toggle = new McmButton() {
            Content = new McmComposite(ICompositeLayout.LayoutGroup.Horizontal, Style) {
                Composites = [
                    new(_on, new(200f, 100f)),
                    new(_off, new(200f, 100f)),
                ],
            },
            OnClick = () => SetValue(!Value),
            DisableGradient = true,
        };
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        Style.Size = new(2000f, 100f);
        Style.LayoutSpacing = new(10f, 10f);
        var group = new McmComposite(ICompositeLayout.LayoutGroup.Horizontal, Style) {
            Composites = [
                .. _entry,
                new(_toggle, new(400f, 100f)),
            ],
        };

        var toggle = group.Render(parent);
        Value = Read();

        return base.Render(toggle);
    }

    public override void Update()
    {
        Debug.Log("update here");
    }
}
