using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;
using UnityEngine.UI;

namespace Mcm.Implementation.Configurables;

internal class McmSlider : McmConfigurable<float>, ISlider
{
    private readonly McmComposite _handle;
    private readonly McmSeparator _line;
    private readonly McmComposite _slider;
    private readonly McmText _valueText;

    public McmSlider(string key, McmSettingEntry entry)
        : base(key, entry.Name, entry.Description, McmStyle.Default())
    {
        Style.Size = new(90f, 90f);
        Style.OutlineSize = new(3f, 3f);

        _valueText = new(Style) {
            Content = Value.ToString(),
        };
        _handle = new(ICompositeLayout.LayoutGroup.Overlap, Style) {
            Composites = [
                new(new McmImage(Style), new(90f, 90f)),
                new(_valueText, new(90f, 90f)),
            ],
        };

        _line = new(new() {
            ColorPrimary = Style.ColorSecondaryVariant,
            Size = new(5f, 5f),
        });
        _slider = new(ICompositeLayout.LayoutGroup.Horizontal) {
            Composites = [
                .. _entry,
                new(_line, McmStyle.SettingLayout.Setting),
            ],
        };
    }

    public Slider? Slider { get; set; }

    public float Min { get; init; }
    public float Max { get; init; }
    public float Step { get; init; }

    public override IBasicEntry.EntryType SettingType => IBasicEntry.EntryType.Slider;

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var sliderGroup = _slider.Render<RectTransform>(parent);


        Value = Read();

        return base.Render(sliderGroup);
    }

    public override void Update()
    {
        Debug.Log($"slidinnnnnnnn {Value}");
    }
}