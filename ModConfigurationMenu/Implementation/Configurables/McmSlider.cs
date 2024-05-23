using ChronoArkMod.Helper;
using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;
using UnityEngine.UI;

namespace Mcm.Implementation.Configurables;

#nullable enable

internal class McmSlider : McmConfigurable<float>, ISlider
{
    public Slider? Slider { get; set; }

    public float Min { get; init; }
    public float Max { get; init; }
    public float Step { get; init; }

    public McmSlider(string key, McmSettingEntry entry) : base(key, entry.Name, entry.Description)
    {

    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }
        
        var group = new McmComposite(ICompositeLayout.LayoutGroup.Horizontal) {
            Composites = [
                .. _entry,
            ],
            Size = new(1000f, 100f),
            Spacing = new(10f, 10f),
        };

        var sliderHolder = group.Render(parent).AttachRectTransformObject("McmSlider");

        Slider = sliderHolder.AddComponent<Slider>();
        Slider.minValue = Min;
        Slider.maxValue = Max;

        Slider.onValueChanged.AddListener(SetValue);
        Slider.onValueChanged.AddListener((_) => Update());

        Value = Read();

        return base.Render(sliderHolder);
    }

    public override void Update()
    {
        Debug.Log($"slidinnnnnnnn {Value}");
    }
}
