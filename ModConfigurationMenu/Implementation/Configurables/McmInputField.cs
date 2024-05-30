using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;
using TMPro;

namespace Mcm.Implementation.Configurables;

public class McmInputField : McmConfigurable<string>, IInputField
{
    private readonly McmImage _bg;
    private readonly McmComposite _configurable;
    private TextMeshProUGUI? _text;
    private TMP_InputField? _tmp;

    public McmInputField(string key, McmSettingEntry entry)
        : base(key, entry.Name, entry.Description, McmStyle.Default())
    {
        var bgStyle = Style with {
            ColorPrimary = Color.white,
            Size = McmStyle.SettingLayout.Setting,
            LayoutPadding = McmStyle.SettingLayout.InputPadding,
            OutlineSize = null,
        };
        _bg = new(bgStyle);

        var configurableStyle = Style with {
            Size = McmStyle.SettingLayout.Setting,
            LayoutSpacing = McmStyle.SettingLayout.SettingSpacingInner,
        };
        _configurable = new McmHorizontal(configurableStyle) {
            Composites = [
                .. _entry,
                new(_bg, configurableStyle.Size.Value),
            ],
        };
    }

    public TMP_InputField.CharacterValidation CharacterValidation { get; init; }
    public override IBasicEntry.EntryType SettingType => IBasicEntry.EntryType.Input;

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var configurable = _configurable.Render<RectTransform>(parent);
        configurable.name = $"McmInputField:{Id}";

        var input = _bg.Ref!.transform.AttachRectTransformObject("McmInput");
        input.SetToStretch();

        var viewport = input.AttachRectTransformObject("McmInputViewport");
        viewport.SetToStretch();

        _tmp = input.AddComponent<TMP_InputField>();
        _tmp.textViewport = viewport;

        _text = viewport.AddComponent<TextMeshProUGUI>();
        _text.color = Color.black;
        _text.alignment = TextAlignmentOptions.Center;
        _text.enableAutoSizing = true;
        _text.fontSizeMin = 24f;
        _text.fontSizeMax = 38f;
        _tmp.textComponent = _text;
        _tmp.caretColor = Color.black;
        _text.overflowMode = TextOverflowModes.ScrollRect;
        _text.enableWordWrapping = true;
        _tmp.characterValidation = CharacterValidation;

        Value = Read();

        // spawn the caret
        _bg.Hide();
        CoroutineHelper.Deferred(() => {
            _bg.Show();
            _tmp.onValueChanged.AddListener(SetValue);
            viewport.sizeDelta = Vector2.zero;
        });

        return base.Render(configurable);
    }

    public override void Update()
    {
        if (_tmp != null) {
            _tmp.text = Value;
        }
    }
}