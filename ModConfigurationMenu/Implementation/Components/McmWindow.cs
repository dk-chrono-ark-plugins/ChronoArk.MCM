using ChronoArkMod;
using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using Mcm.Api.Configurables;
using UnityEngine.EventSystems;

namespace Mcm.Implementation.Components;

#nullable enable

internal class McmWindow : UIBehaviour
{
    private static bool _onceFlag;
    private readonly List<IPage> _pageHierarchy = [];
    private bool _shouldReturn = true;
    private Canvas? _canvas;

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
    public static IModLayout? MyLayout { get; private set; }

    protected override void Start()
    {
        Instance = this;
        LookupOnce();
        _canvas = RenderHelper.Setup(gameObject);
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
        this.StartDeferredCoroutine(() => gameObject.SetActive(false));
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
        McmManager.SaveMcmConfig(Instance.TopPage.Owner);
        Instance.TopPage.Elements
            .OfType<INotifyChange>()
            .Do(disp => disp.NotifyApply());
    }

    public static void Reset()
    {
        if (Instance == null || Instance.TopPage == null) {
            return;
        }
        McmManager.ResetMcmConfig(Instance.TopPage.Owner);
        Instance.TopPage.Elements
            .OfType<INotifyChange>()
            .Do(disp => disp.NotifyReset());
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
            TopPage?.Show();
            throw;
        }
        _pageHierarchy.Add(page);
    }

    public void RenderNamedPage(ModInfo modInfo, string name)
    {
        var registry = McmManager.GetMcmRegistry(modInfo);
        this.StartDeferredCoroutine(() => RenderPage(registry!.Layout.GetPage(name)));
    }

    public void RenderIndexPage(ModInfo modInfo)
    {
        RenderNamedPage(modInfo, "index");
    }

    private void RenderSelf()
    {
        var modInfo = ModManager.getModInfo(McmMod.Instance!.ModId);
        var layout = McmManager.GetMcmRegistry(modInfo)!.Layout;
        var myPage = layout.GetPage("McmEntry") ??
            throw new InvalidOperationException($"MCM cannot render the entry page...");

        myPage.Clear();
        McmManager.Instance
            .PopulateModEntries()
            .ForEach(myPage.Add);

        RenderPage(myPage);
    }

    private void LookupOnce()
    {
        if (ModUI == null && !_onceFlag &&
            ComponentFetch.TryFindObject<ModUI>("ModUI", out var modUI)) {
            ModUI = modUI;
        }
        MyLayout = McmManager.GetMcmRegistry(McmMod.ModInfo)?.Layout;
        _onceFlag = true;
    }
}
