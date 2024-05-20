using ChronoArkMod;
using ChronoArkMod.Helper;
using ChronoArkMod.ModData;
using MCM.Api.Displayables;
using MCM.Implementation.Displayables;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MCM.Implementation.Components;

#nullable enable

internal class McmWindow : UIBehaviour
{
    public const string ButtonEntryName = "MCM Button";
    public const string ButtonEntryText = "Mods";

    private bool _shouldReturn = true;

    private Canvas? _canvas;
    private IModLayout? _currentRendering;

    public static McmWindow? Instance;

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
        _currentRendering?.CurrentPage.Hide();
        page.Render(_canvas.transform);
    }

    public void RenderIndexPage(ModInfo mod)
    {
        var registry = McmManager.Instance.Registries[mod];
        RenderPage(registry.Layout.IndexPage);
        _currentRendering = registry.Layout;
    }

    private void RenderSelf()
    {
        var modInfo = ModManager.getModInfo(ModConfigurationMenuMod.Instance!.ModId);
        var myPage = McmManager.Instance.Registries[modInfo].Layout.IndexPage;
        myPage.Clear();

        ModManager.LoadedMods
            .Select(ModManager.getModInfo)
            .Do(mod => myPage.Add(new McmModEntry(mod)));
        myPage.Add(new McmButton() {
            Content = new McmImage() { MaskColor = Color.red },
            OnClick = () => Debug.Log("Clicked!")
        });
        RenderPage(myPage);
    }
}
