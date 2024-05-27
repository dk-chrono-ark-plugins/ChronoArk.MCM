using System.Diagnostics;

namespace Mcm.Implementation;

internal static class McmMockup
{
    internal static void Mockup(IModLayout layout)
    {
        // mcm mockup
        var mockup = layout.AddPage("McmMockup", ICompositeLayout.LayoutGroup.Vertical, true);
        mockup.Title = McmLoc.Mockup.Title;

        mockup.AddText(McmLoc.Entry.Version).Content += McmManager.McmInstanceVersion;
        mockup.AddText(McmLoc.Mockup.Intro);
        mockup.AddSeparator();
        mockup.AddText(McmLoc.Mockup.Displayable);
        mockup.AddSeparator();

        mockup.AddText(McmLoc.Mockup.ModLayout);
        mockup.AddText(McmLoc.Mockup.Usage);
        mockup.AddSeparator();
        mockup.AddText(McmLoc.Mockup.Configurable);
        mockup.AddText(McmLoc.Mockup.McmPage);
        mockup.AddText(McmLoc.Mockup.CustomPage);
        mockup.AddSeparator();

        mockup.AddText(McmLoc.Mockup.Text);
        mockup.AddSeparator();
        mockup.AddText(McmLoc.Mockup.Loc);
        mockup.AddText(McmLoc.Mockup.LoremIpsum);
        mockup.AddSeparator();
        mockup.AddText(McmLoc.Mockup.Separator);
        mockup.AddSeparator();

        mockup.AddText(McmLoc.Mockup.Image);
        mockup.AddImage("cover.png");
        mockup.AddText(McmLoc.Mockup.Absens);
        mockup.AddImage("absss.png");
        mockup.AddSeparator();

        mockup.AddText(McmLoc.Mockup.Composite);
        mockup.AddSeparator();

        mockup.AddText(McmLoc.Mockup.Outro);
        mockup.AddButton("Github", () => { Process.Start("https://github.com/dk-chrono-ark-plugins"); });
    }

    internal static void Stub(IModLayout layout)
    {
        McmMod.ModInfo?.StubMcmPage();
        layout.IndexPage.AddSeparator();
    }

    internal static void NormalRegister(IModLayout layout)
    {
        var index = layout.IndexPage;

        index.AddText(McmLoc.Entry.Version).Content += McmManager.McmInstanceVersion;
        index.AddSeparator();

        var config = McmMod.Instance?._config ?? new();
        layout.AddToggleOption("AttachDebugScriptRef",
            McmLoc.Index.AttachDebugScriptRefName,
            McmLoc.Index.AttachDebugScriptRefDesc,
            config.AttachDebugComponent,
            value => config.AttachDebugComponent = value);
    }
}