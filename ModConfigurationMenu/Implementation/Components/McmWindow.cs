using ChronoArkMod;
using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using Mcm.Api.Configurables;
using Mcm.Implementation.Configurables;
using Mcm.Implementation.Displayables;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ChronoArkMod.ModEditor.Console.ConsoleManager;

namespace Mcm.Implementation.Components;

#nullable enable

internal class McmWindow : UIBehaviour
{
    private static bool _onceFlag;
    private bool _shouldReturn = true;
    private Canvas? _canvas;
    private readonly List<IPage> _pageHierarchy = [];

    public IPage? TopPage
    {
        get
        {
            _pageHierarchy.RemoveAll(page => page is IScriptRef @ref && @ref.Ref == null);
            return _pageHierarchy.LastOrDefault();
        }
    }
    public static McmWindow? Instance { get; private set; }
    public static ModUI? ModUI { get; private set; }

    protected override void Start()
    {
        Instance = this;
        LookupOnce();

        _canvas = gameObject.GetOrAddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        gameObject.GetOrAddComponent<CanvasScaler>();
        gameObject.GetOrAddComponent<GraphicRaycaster>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Back();
        }
    }

    protected override void OnDestroy()
    {
        Instance = null;
        base.OnDestroy();
    }

    public void Open()
    {
        var mom = GameObject.Find("MainOptions");
        if (mom != null) {
            gameObject.SetActive(true);
            mom.SetActive(false);
            _shouldReturn = false;

            this.StartDeferredCoroutine(
                () => mom.SetActive(true),
                () => _shouldReturn
            );
            // render on next frame
            this.StartDeferredCoroutine(RenderSelf);
        }
    }

    public void Close()
    {
        _shouldReturn = true;
        CoroutineHelper.HaltAll();
        this.StartDeferredCoroutine(() => gameObject.SetActive(false));
    }

    public void RenderPage(IPage? page)
    {
        if (!gameObject.activeInHierarchy || _canvas == null || page == null) {
            return;
        }
        _shouldReturn = false;

        try {
            TopPage?.Hide();
            page.Render(_canvas.transform);
        } catch {
            Debug.Log($"failed to render page {page.Title}");
            throw;
        }
        _pageHierarchy.Add(page);
    }

    public void RenderIndexPage(ModInfo modInfo)
    {
        RenderNamedPage(modInfo, "index");
    }

    public void RenderNamedPage(ModInfo modInfo, string name)
    {
        var registry = McmManager.GetMcmRegistry(modInfo);
        this.StartDeferredCoroutine(() => RenderPage(registry!.Layout.GetPage(name)));
    }

    public static void Back()
    {
        if (Instance == null) {
            return;
        }

        Instance.TopPage?.Destroy();
        if (Instance.TopPage == null) {
            Instance.Close();
        } else {
            Instance.TopPage.Show();
        }
    }

    public static void Save()
    {
        if (Instance == null || Instance.TopPage == null) {
            return;
        }
        McmManager.SaveModSetting(Instance.TopPage.Owner);
        Instance.TopPage.Elements
            .OfType<INotifyChanged>()
            .Do(disp => disp.NotifyApply());
    }

    public static void Reset()
    {
        if (Instance == null || Instance.TopPage == null) {
            return;
        }
        McmManager.ResetModSetting(Instance.TopPage.Owner);
        Instance.TopPage.Elements
            .OfType<INotifyChanged>()
            .Do(disp => disp.NotifyReset());
    }

    private void RenderSelf()
    {
        var modInfo = ModManager.getModInfo(McmMod.Instance!.ModId);
        var layout = McmManager.GetMcmRegistry(modInfo)!.Layout;
        var myPage = layout.GetPage("McmEntry") ??
            throw new InvalidOperationException($"MCM cannot render the entry page...");
        
        myPage.Clear();
        PopulateModIndexes(myPage);

        RenderPage(myPage);
    }

    private void PopulateModIndexes(IPage myPage)
    {
        Debug.Log($"populating mcm index pages");

        var indexes = new List<McmModEntry>();
        foreach (var modInfo in ModManager.LoadedMods.Select(ModManager.getModInfo)) {
            try {
                var registry = McmManager.GetMcmRegistry(modInfo);
                if (registry == null) {
                    McmManager.Instance.Register(modInfo.id);
                    if (modInfo.settings.Count > 0) {
                        Debug.Log($"{modInfo.id} has legacy settings and is not registered with MCM");
                        Debug.Log("attempt to generate a stub page...");
                        modInfo.StubMcmPage();
                    }
                    registry = McmManager.GetMcmRegistry(modInfo)
                        ?? throw new InvalidOperationException($"cannot populate mod index page for {modInfo.id}");
                }
                var modEntry = new McmModEntry(modInfo);
                modEntry.ModEntry.Interactable = registry.Settings.Count > 0;
                indexes.Add(modEntry);
            } catch (Exception ex) {
                Debug.Log($"failed: {ex.Message}");
                // noexcept
            }
        }

        indexes.OrderByDescending(e => e.ModEntry.Interactable)
            .ThenBy(e => e.Owner.id)
            .Do(myPage.Add);
    }

    private void LookupOnce()
    {
        if (ModUI == null && !_onceFlag &&
            ComponentFetch.TryFindObject<ModUI>("ModUI", out var modUI)) {
            ModUI = modUI;
        }
        _onceFlag = true;
    }
}
