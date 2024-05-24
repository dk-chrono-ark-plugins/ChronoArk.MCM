using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;
using UnityEngine.UI;

namespace Mcm.Implementation.Configurables;

#nullable enable

internal class McmSlider : McmConfigurable<float>, ISlider
{
    private readonly McmImage _bg;

    public float Min { get; init; }
    public float Max { get; init; }
    public float Step { get; init; }
    public Slider? Slider { get; set; }

    public McmSlider(string key, McmSettingEntry entry) : base(key, entry.Name, entry.Description)
    {
        _bg = new McmImage();
        _bg.Style.BorderSize = new(0f, 0f);
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var group = new McmComposite(ICompositeLayout.LayoutGroup.Horizontal) {
            Composites = [
                .. _entry,
                new(_bg, new(800f, 100f))
            ],
        };
        group.Style.LayoutSpacing = new(10f, 10f);
        group.Style.Size = new(2000f, 100f);

        var sliderGroup = group.Render(parent);

        var sliderHolder = _bg.Ref!;
        Slider = sliderHolder.AddComponent<Slider>();
        Slider.minValue = Min;
        Slider.maxValue = Max;
        Slider.targetGraphic = _bg.Image!;

        // Create a new GameObject for the fill area
        GameObject fillArea = new("Fill Area");
        fillArea.transform.SetParent(Slider.transform);
        var rectTransform = fillArea.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0.25f);
        rectTransform.anchorMax = new Vector2(1, 0.75f);
        rectTransform.offsetMin = new Vector2(5, 0);
        rectTransform.offsetMax = new Vector2(-5, 0);

        // Create a new GameObject for the fill
        GameObject fill = new("Fill");
        fill.transform.SetParent(fillArea.transform);
        rectTransform = fill.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);

        // Add an Image component to the fill
        Image fillImage = fill.AddComponent<Image>();
        fillImage.color = new Color(0.25f, 0.5f, 0.75f);

        // Assign the fill to the slider
        Slider.fillRect = fillImage.rectTransform;

        // Create a new GameObject for the handle
        GameObject handle = new("Handle");
        handle.transform.SetParent(Slider.transform);
        rectTransform = handle.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(20, 20);

        // Add an Image component to the handle
        Image handleImage = handle.AddComponent<Image>();
        handleImage.color = new Color(0.75f, 0.75f, 0.75f);

        // Assign the handle to the slider
        Slider.handleRect = handleImage.rectTransform;

        // Set the handle to be draggable
        Slider.direction = Slider.Direction.LeftToRight;

        Slider.onValueChanged.AddListener(SetValue);
        Slider.onValueChanged.AddListener((_) => Update());

        Value = Read();

        return base.Render(sliderGroup);
    }

    public override void Update()
    {
        Debug.Log($"slidinnnnnnnn {Value}");
    }
}
