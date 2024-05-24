using ChronoArkMod.ModData;
using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation.Configurables;

#nullable enable

internal class McmConfigurable<T> : McmStylable, IConfigurable<T>
{
    protected T _value = default!;
    protected readonly ICompositeLayout.Composite[] _entry;
    private readonly McmText _name;
    private bool _notified;

    public virtual string Id { get; init; }
    public virtual string Name { get; init; }
    public virtual string Description { get; init; }
    public virtual required Action<T> Save { get; init; }
    public virtual required Func<T> Read { get; init; }
    public virtual required ModInfo Owner { get; init; }
    public virtual T Value
    {
        get => _value;
        set
        {
            _value = value;
            DeferredUpdate();
        }
    }
    public virtual IBasicEntry.EntryType SettingType => throw new NotImplementedException();

    protected McmConfigurable(string key, string name, string desc, McmStyle? styleOverride = null)
        : base(styleOverride)
    {
        Id = key;
        Name = name;
        Description = desc;

        Style.TextFontSize = 34f;
        Style.Size = McmStyle.SettingLayout.NameText;
        _name = new McmText(Style) {
            Content = name,
        };

        Style.TextFontSize = 30f;
        Style.Size = McmStyle.SettingLayout.DescText;
        var _desc = new McmText(Style) {
            Content = Description,
        };

        _entry = [
            new(_name, McmStyle.SettingLayout.NameText),
            new(_desc, McmStyle.SettingLayout.DescText),
        ];
    }

    public virtual void SetValue(T value)
    {
        Value = value;
        Save(_value);
        NotifyChange();
    }

    public void NotifyChange(object? payload = null)
    {
        if (!_notified) {
            _notified = true;
            AddPrefix();
        }
    }

    public void NotifyApply(object? payload = null)
    {
        if (_notified) {
            _notified = false;
            RemovePrefix();
        }
    }

    public void NotifyReset(object? payload = null)
    {
        Value = Read();
        _notified = false;
        RemovePrefix();
    }

    private void AddPrefix()
    {
        if (_name == null) {
            return;
        }
        if (!_name.Content.StartsWith(McmLoc.Setting.Changed)) {
            _name.Content = McmLoc.Setting.Changed + _name.Content;
        }
    }

    private void RemovePrefix()
    {
        if (_name == null) {
            return;
        }
        if (_name.Content.StartsWith(McmLoc.Setting.Changed)) {
            _name.Content = _name.Content[McmLoc.Setting.Changed.Length..];
        }
    }
}
