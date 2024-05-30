using Mcm.Api.Configurables;
using Mcm.Implementation.Displayables;

namespace Mcm.Implementation.Configurables;

public class McmDropdown : McmConfigurable<int>, IDropdown
{
    private readonly McmComposite _configurable;
    private readonly McmText _current;
    private readonly McmVertical _listOptions;
    private readonly Func<string[]> _options;

    public McmDropdown(string key, Func<string[]> options, McmSettingEntry entry)
        : base(key, entry.Name, entry.Description, McmStyle.Default())
    {
        _options = options;

        var listStyle = Style with {
            Size = McmStyle.SettingLayout.Setting,
            LayoutPadding = new(),
            LayoutSpacing = Vector2.zero,
            OutlineSize = new(3f, 3f),
        };
        _listOptions = new(listStyle) {
            Composites = [],
        };

        var buttonStyle = Style with {
            Size = McmStyle.SettingLayout.Setting,
            LayoutPadding = McmStyle.SettingLayout.InputPadding,
            LayoutSpacing = Vector2.zero,
            OutlineSize = new(3f, 3f),
        };
        _current = new(buttonStyle) {
            Content = McmLoc.Setting.Uninitialized,
        };
        var currentButton = new McmButton(buttonStyle) {
            Content = _current,
            OnClick = _listOptions.Show,
        };
        var currentGroup = new McmHorizontal(buttonStyle) {
            Composites = [
                new(currentButton, buttonStyle.Size.Value),
            ],
        };

        var configurableStyle = Style with {
            Size = McmStyle.SettingLayout.Setting,
            LayoutSpacing = McmStyle.SettingLayout.SettingSpacingInner,
        };
        _configurable = new McmHorizontal(configurableStyle) {
            Composites = [
                .. _entry,
                new(currentGroup, configurableStyle.Size.Value),
            ],
        };
    }

    public string[] Options => _options();
    public override IBasicEntry.EntryType SettingType => IBasicEntry.EntryType.Dropdown;

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var configurable = _configurable.Render<RectTransform>(parent);
        configurable.name = $"McmDropdown:{Id}";

        PopulateOptions();
        var list = _listOptions.Render<RectTransform>(parent);
        list.name = $"McmDropdownList:{Id}";
        CoroutineHelper.Deferred(_listOptions.Hide);

        Value = Read();

        return base.Render(configurable);
    }

    public override void Update()
    {
        _current.Content = Options[Value];
    }

    private void PopulateOptions()
    {
        _listOptions.Composites?.Clear();
        for (var i = 0; i < Options.Length; ++i) {
            var index = i;
            var option = new McmButton(_listOptions.Style) {
                Content = new McmText(_listOptions.Style) {
                    Content = Options[index],
                },
                OnClick = () => {
                    SetValue(index);
                    CoroutineHelper.Deferred(_listOptions.Hide);
                },
            };
            _listOptions.Composites?.Add(new(option, option.Style.Size!.Value));
        }
    }
}