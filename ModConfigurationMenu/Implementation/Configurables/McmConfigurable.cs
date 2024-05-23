using ChronoArkMod.ModData;
using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation.Configurables;

#nullable enable

internal class McmConfigurable<T> : ScriptRef, IConfigurable<T>
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

    public IBasicEntry.EntryType SettingType { get; init; }

    protected McmConfigurable(string key, string name, string desc)
    {
        Id = key;
        Name = name;
        Description = desc;

        _name = new McmText() {
            Content = name,
            FontSize = 30f,
            Size = new(400f, 100f),
        };
        var _desc = new McmText() {
            Content = Description,
            Size = new(400f, 100f),
            FontSize = 30f,
        };
        _entry = [
            new(_name, new(400f, 100f)),
            new(_desc, new(400f, 100f)),
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
        if (!_name.Content.StartsWith(McmLoc.Page.Changed)) {
            _name.Content = McmLoc.Page.Changed + _name.Content;
        }
    }

    private void RemovePrefix()
    {
        if (_name == null) {
            return;
        }
        if (_name.Content.StartsWith(McmLoc.Page.Changed)) {
            _name.Content = _name.Content[McmLoc.Page.Changed.Length..];
        }
    }
}
