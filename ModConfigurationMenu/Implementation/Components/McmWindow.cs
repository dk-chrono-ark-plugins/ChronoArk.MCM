using ChronoArkMod;
using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using Mcm.Api.Configurables;
using Mcm.Implementation.Configurables;
using Mcm.Implementation.Displayables;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
            .Do(disp => disp.NotifyApplied());
    }

    public static void Reset()
    {
        if (Instance == null || Instance.TopPage == null) {
            return;
        }
        McmManager.ResetModSetting(Instance.TopPage.Owner);
        Instance.TopPage.Elements
            .OfType<INotifyChanged>()
            .Do(disp => disp.NotifyApplied());
    }

    private void RenderSelf()
    {
        var modInfo = ModManager.getModInfo(McmMod.Instance!.ModId);
        var layout = McmManager.GetMcmRegistry(modInfo)!.Layout;
        var myPage = layout.GetPage("McmEntry") ??
            throw new InvalidOperationException($"MCM cannot render the entry page...");
        myPage.Clear();

        // populate mod entries
        ModManager.LoadedMods
            .Select(ModManager.getModInfo)
            .Select(mod => new McmModEntry(mod))
            .Do(myPage.Add);

        RenderPage(myPage);
    }

    private void RenderMockup2()
    {
        var modInfo = ModManager.getModInfo(McmMod.Instance!.ModId);
        var registry = McmManager.GetMcmRegistry(modInfo);
        var myPage = registry!.Layout.GetPage("index") ??
            throw new InvalidOperationException($"MCM cannot render the entry page...");
        myPage.Clear();

        registry!.Settings = modInfo.StubMcmConfig();
        var setting = registry.Settings;
        var key = "TestToggleKey";
        var entry = setting[key];
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
