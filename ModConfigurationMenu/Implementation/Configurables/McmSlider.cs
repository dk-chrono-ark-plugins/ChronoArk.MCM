using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;
using UnityEngine.UI;

namespace Mcm.Implementation.Configurables;

internal class McmSlider : McmConfigurable<float>, ISlider
{
    private readonly McmComposite _configurable;
    private readonly McmLayerText _handle;
    private readonly McmSeparator _line;

    public McmSlider(string key, McmSettingEntry entry)
        : base(key, entry.Name, entry.Description, McmStyle.Default())
    {
        var handleStyle = Style with {
            Size = new(100f, 50f),
            OutlineSize = new(3f, 3f),
        };
        _handle = new(new McmImage(handleStyle), handleStyle);

        _line = new(new() {
            ColorPrimary = Style.ColorSecondaryVariant,
            Size = new(5f, 5f),
        });
        var sliderStyle = Style with {
            Size = McmStyle.SettingLayout.Setting,
            LayoutPadding = McmStyle.SettingLayout.SliderPadding,
        };
        var lineRange = new McmHorizontal(sliderStyle) {
            Composites = [
                new(_line, sliderStyle.Size.Value),
            ],
        };

        var configurableStyle = Style with {
            Size = McmStyle.SettingLayout.Setting,
            LayoutSpacing = McmStyle.SettingLayout.SettingSpacingInner,
        };
        _configurable = new McmHorizontal(configurableStyle) {
            Composites = [
                .. _entry,
                new(lineRange, configurableStyle.Size.Value),
            ],
        };
    }

    public Slider? Slider { get; private set; }

    public float Min { get; init; }
    public float Max { get; init; }
    public float Step { get; init; }

    public override IBasicEntry.EntryType SettingType => IBasicEntry.EntryType.Slider;

    public override void SetValue(float value)
    {
        var valueClamped = Mathf.Round(value / Step) * Step;
        base.SetValue(valueClamped);
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var configurable = _configurable.Render(parent);
        configurable.name = $"McmSlider:{Id}";

        Slider = _line.Ref!.AddComponent<Slider>();
        Slider.direction = Slider.Direction.LeftToRight;
        Slider.handleRect = _handle.Render<RectTransform>(_line.Ref.transform);
        Slider.targetGraphic = null;
        Slider.fillRect = null;

        Slider.minValue = Min;
        Slider.maxValue = Max;
        Value = Mathf.Round(Read() / Step) * Step;
        Slider.value = Value;
        Slider.onValueChanged.AddListener(SetValue);

        return base.Render(configurable);
    }

    public override void Update()
    {
        _handle.Content = Value.ToString("F2");
        if (Slider != null) {
            Slider.value = Value;
        }
    }
}