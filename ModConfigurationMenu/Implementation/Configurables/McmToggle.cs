using ChronoArkMod.Helper;
using I2.Loc;
using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation.Configurables;

#nullable enable

internal class McmToggle : McmConfigurable<bool>, IToggle
{
    private readonly McmComposite _on;
    private readonly McmComposite _off;
    private Transform? _settingEntry;
    private bool _value;

    public override bool Value
    {
        get => _value;
        set
        {
            _value = value;
            DeferredUpdate();
        }
    }

    public McmToggle(string key, McmSettingEntry entry) : base(key, entry.Name, entry.Description)
    {
        _on = new McmComposite(ICompositeLayout.LayoutGroup.Overlap) {
            Composites = [
                new(new McmImage() { MaskColor = Color.black }, new(400f, 100f)),
                new(new McmText() {
                        Content = LocalizationManager.GetTranslation(ScriptTerms.UI_Option.Bool_On),
                        FontSize = 50f,
                    },
                    new(400f, 100f)),
            ],
            Size = new(400f, 100f),
        };
        _off = new McmComposite(ICompositeLayout.LayoutGroup.Overlap) {
            Composites = [
                new(new McmImage(){ MaskColor = Color.black }, new(400f, 100f)),
                new(new McmText() {
                        Content = LocalizationManager.GetTranslation(ScriptTerms.UI_Option.Bool_Off),
                        FontSize = 50f,
                    },
                    new(400f, 100f)),
            ],
            Size = new(400f, 100f),
        };
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var button = new McmButton() {
            Content = new McmComposite(ICompositeLayout.LayoutGroup.Horizontal) {
                Composites = [
                    new(_on, new(400f, 100f)),
                    new(_off, new(400f, 100f)),
                ],
                Size = new(800f, 100f),
                Padding = new(5, 5, 10, 10),
                Spacing = new(5f, 0f),
            },
            OnClick = () => SetState(!Value),
        };
        if (button.Background is McmImage bg) {
            bg.BorderColor = Color.clear;
            bg.BorderThickness = new(0f, 0f);
        }

        MergeSetting(new(button, new(800f, 100f)));

        var group = base.Render(parent);

        Value = Read();
        _settingEntry = group;

        return group;
    }

    public void SetState(bool state)
    {
        Value = state;
        Save(_value);
        NotifyChange();
    }

    public override void DeferredUpdate()
    {
        if (_deferred) {
            return;
        }
        _deferred = true;
        CoroutineHelper.Deferred(
            () => {
                UpdateVisual();
                _dirty = true;
                _deferred = false;
            },
            () => _settingEntry != null
        );
    }

    private void UpdateVisual()
    {
        if (_on.Composites[0].Displayable is McmImage on) {
            on.BorderColor = _value ? Color.white : Color.clear;
            on.BorderThickness = _value ? new(5f, 5f) : null;
        }
        if (_off.Composites[0].Displayable is McmImage off) {
            off.BorderColor = !_value ? Color.white : Color.clear;
            off.BorderThickness = !_value ? new(5f, 5f) : null;
        }
    }
}
