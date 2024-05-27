using ChronoArkMod.ModData;
using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;
using TMPro;

namespace Mcm.Implementation.Configurables;

/// <summary>
///     A configurable entry that has name and description ready, but not displayable.
/// </summary>
/// <remarks>Must be derived from and render inclusively using <see cref="McmHorizontal" /></remarks>
/// <typeparam name="T"></typeparam>
public class McmConfigurable<T> : McmStylable, IConfigurable<T>
{
    protected readonly ICompositeLayout.Composite[] _entry;
    private readonly McmText _name;
    private bool _notified;
    private T _value = default!;

    protected McmConfigurable(string key, string name, string desc, McmStyle? styleOverride = null)
        : base(styleOverride)
    {
        Id = key;
        Name = name;
        Description = desc;

        var textStyle = Style with {
            Size = McmStyle.SettingLayout.NameText,
            TextFontSize = 34f,
            TextAlignment = TextAlignmentOptions.Left,
        };
        _name = new(textStyle) {
            Content = name,
        };

        var descStyle = textStyle with {
            Size = McmStyle.SettingLayout.DescText,
            TextAutoSize = true,
        };
        var descText = new McmText(descStyle) {
            Content = Description,
        };

        _entry = [
            new(_name, McmStyle.SettingLayout.NameText),
            new(descText, McmStyle.SettingLayout.DescText),
        ];
    }

    public required ModInfo Owner { get; init; }

    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public virtual required Action<T> Save { get; init; }
    public virtual required Func<T> Read { get; init; }

    public virtual T Value
    {
        get => _value;
        protected set
        {
            _value = value;
            DeferredUpdate();
        }
    }

    public virtual IBasicEntry.EntryType SettingType => throw new NotImplementedException();

    public virtual void SetValue(T value)
    {
        Value = value;
        Save(_value);
        NotifyChange();
    }

    public virtual void NotifyChange(object? payload = null)
    {
        if (_notified) {
            return;
        }

        AddPrefix();
        CoroutineHelper.Deferred(() => { _notified = true; });
    }

    public virtual void NotifyApply(object? payload = null)
    {
        if (!_notified) {
            return;
        }

        RemovePrefix();
        CoroutineHelper.Deferred(() => { _notified = false; });
    }

    public virtual void NotifyReset(object? payload = null)
    {
        Value = Read();
        RemovePrefix();
        CoroutineHelper.Deferred(() => { _notified = false; });
    }

    private void AddPrefix()
    {
        if (!_name.Content.StartsWith(McmLoc.Setting.Changed)) {
            _name.Content = McmLoc.Setting.Changed + _name.Content;
        }
    }

    private void RemovePrefix()
    {
        if (_name.Content.StartsWith(McmLoc.Setting.Changed)) {
            _name.Content = _name.Content[McmLoc.Setting.Changed.Length..];
        }
    }
}