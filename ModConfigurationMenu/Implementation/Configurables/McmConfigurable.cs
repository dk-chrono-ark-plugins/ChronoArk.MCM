using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;
using System.ComponentModel;

namespace Mcm.Implementation.Configurables;

#nullable enable

internal class McmConfigurable<T> : ScriptRef, IConfigurable<T>
{
    public const string NotifyChangePrefix = "<b>*</b> ";

    private readonly McmComposite _entry;
    private McmText? _name;
    private bool _notified;

    public virtual string Id { get; init; }
    public virtual string? Name { get; set; }
    public virtual string? Description { get; set; }
    public virtual required Action<T> Save { get; init; }
    public virtual required Func<T> Read { get; init; }
    public virtual required ModInfo Owner { get; init; }
    public virtual T Value
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
    public IBasicEntry.EntryType SettingType { get; init; }

    protected McmConfigurable(string key, string name, string description)
    {
        Id = key;
        Name = name;
        Description = description;

        _entry = new(ICompositeLayout.LayoutGroup.Horizontal) {
            Composites = [],
            Size = new(1000f, 100f),
            Spacing = new(10f, 10f),
        };
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        _name = new McmText() {
            Content = Name ?? "Unknown Setting",
            FontSize = 30f,
            Size = new(400f, 100f),
        };
        var desc = new McmText() {
            Content = Description ?? string.Empty,
            Size = new(400f, 100f),
            FontSize = 30f,
        };
        _entry.Composites = [
            new(_name, new(400f, 100f)),
            new(desc, new(400f, 100f)),
            _entry.Composites[0],
        ];

        var option = _entry.Render<RectTransform>(parent);

        return base.Render(option);
    }

    public void NotifyChanged()
    {
        if (!_notified) {
            _name!.Content = NotifyChangePrefix + _name.Content;
            _notified = true;
        }
    }

    public void NotifyApplied()
    {
        if (_notified && _name!.Content.StartsWith(NotifyChangePrefix)) {
            _name.Content = _name.Content[NotifyChangePrefix.Length..];
            _notified = false;
        }
    }

    protected void MergeSetting(ICompositeLayout.Composite composite)
    {
        _entry.Composites = [composite];
    }
}
