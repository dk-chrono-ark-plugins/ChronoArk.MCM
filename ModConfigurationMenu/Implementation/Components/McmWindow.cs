using ChronoArkMod;
using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using Mcm.Implementation.Displayables;
using Microsoft.Win32;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mcm.Implementation.Components;

#nullable enable

internal class McmWindow : UIBehaviour
{
    private bool _shouldReturn = true;
    private Canvas? _canvas;

    public IModLayout? CurrentRendering { get; set; }
    public static McmWindow? Instance { get; private set; }

    private void Start()
    {
        Instance = this;

        _canvas = gameObject.GetOrAddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        gameObject.GetOrAddComponent<CanvasScaler>();
        gameObject.GetOrAddComponent<GraphicRaycaster>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            Close();
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void Open()
    {
        var mom = GameObject.Find("MainOptions")?.GetComponent<MainOptionMenu>();
        if (mom != null) {
            gameObject.SetActive(true);
            mom.gameObject.SetActive(false);
            _shouldReturn = false;

            this.StartDeferredCoroutine(
                () => mom.gameObject.SetActive(true),
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

    public void RenderPage(IPage page)
    {
        if (!gameObject.activeInHierarchy || _canvas == null) {
            return;
        }
        _shouldReturn = false;
        CurrentRendering?.CurrentPage.Hide();
        page.Render(_canvas.transform);
    }

    public void RenderIndexPage(ModInfo mod)
    {
        var registry = McmManager.Instance.Registries[mod];
        RenderPage(registry.Layout.IndexPage);
        CurrentRendering = registry.Layout;
    }

    public static void Save()
    {
        if (Instance is null || Instance.CurrentRendering is null) {
            return;
        }
        var modInfo = Instance.CurrentRendering.Owner;
        $"Saving {modInfo.Title}".Log();
        McmManager.Instance.Registries[modInfo].Save?.Invoke();
    }

    public static void Reset()
    {
        if (Instance is null || Instance.CurrentRendering is null) {
            return;
        }
        var modInfo = Instance.CurrentRendering.Owner;
        $"Applying {modInfo.Title}".Log();
        McmManager.Instance.Registries[modInfo].Reset?.Invoke();
    }

    private void RenderSelf()
    {
        var modInfo = ModManager.getModInfo(ModConfigurationMenuMod.Instance!.ModId);
        var layout = McmManager.Instance.Registries[modInfo].Layout;
        var myPage = layout.GetPage("McmEntry") ?? throw new InvalidOperationException($"MCM cannot render the entry page...");
        myPage.Clear();

        // populate mod entries
        ModManager.LoadedMods
            .Select(ModManager.getModInfo)
            .Select(mod => new McmModEntry(mod))
            .Do(myPage.Add);

        RenderPage(myPage);
        CurrentRendering = layout;
    }
}
