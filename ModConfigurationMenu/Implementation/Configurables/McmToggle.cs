using ChronoArkMod.Helper;
using I2.Loc;
using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;
using UnityEngine.UI;

namespace Mcm.Implementation.Configurables;

#nullable enable

internal class McmToggle : McmConfigurable<bool>, IToggle
{
    private readonly McmComposite _on;
    private readonly McmComposite _off;
    private readonly McmButton _toggle;
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
                new(new McmImage() { 
                        MaskColor = Color.black,
                        BorderColor = Color.clear,
                        BorderThickness = new(0f, 0f),
                    }, 
                    new(400f, 100f)),
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
                new(new McmImage(){ 
                        MaskColor = Color.black,
                        BorderColor = Color.clear,
                        BorderThickness = new(0f, 0f), 
                    }, 
                    new(400f, 100f)),
                new(new McmText() {
                        Content = LocalizationManager.GetTranslation(ScriptTerms.UI_Option.Bool_Off),
                        FontSize = 50f,
                    },
                    new(400f, 100f)),
            ],
            Size = new(400f, 100f),
        };
        _toggle = new McmButton() {
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
            DisableGradient = true,
        };
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        MergeSetting(new(_toggle, new(800f, 100f)));
        CoroutineHelper.Deferred(() => Value = Read());

        return base.Render(parent);
    }

    public void SetState(bool state)
    {
        Value = state;
        Save(_value);
        NotifyChange();
    }

    public override void Update()
    {
        if (_on.Composites[0].Displayable is McmImage on) {
            on.Image!.GetComponent<Outline>().effectColor = _value ? Color.white : Color.black;
            on.Image!.GetComponent<Outline>().effectDistance = _value ? new(5f, 5f) : Vector2.zero;
        }
        if (_off.Composites[0].Displayable is McmImage off) {
            off.Image!.GetComponent<Outline>().effectColor = !_value ? Color.white : Color.black;
            off.Image!.GetComponent<Outline>().effectDistance = !_value ? new(5f, 5f) : Vector2.zero;
        }
        if (_toggle.Background is McmImage bg) {
            bg.Image!.GetComponent<Outline>().effectColor = Color.clear;
            bg.Image!.GetComponent<Outline>().effectDistance = Vector2.zero;
        }
    }
}
