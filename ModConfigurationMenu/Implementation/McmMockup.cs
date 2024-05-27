namespace Mcm.Implementation;

internal static class McmMockup
{
    internal static void Mockup(IModLayout layout)
    {
        // mcm mockup
        var mockup = layout.AddPage("McmMockup", ICompositeLayout.LayoutGroup.Vertical, true);
        mockup.Title = "Mcm Mockup & Playground";
        mockup.AddText(McmManager.McmInstanceVersion.ToString());
        mockup.AddSeparator();
        mockup.AddImage("cover.png");
        mockup.AddSeparator();
        mockup.AddText("okay this is text component");
        mockup.AddImage("absss.png");
        mockup.AddSeparator();
    }

    internal static void Stub(IModLayout layout)
    {
        CoroutineHelper.Deferred(
            () => {
                McmMod.ModInfo?.StubMcmPage();

                layout.IndexPage.AddSeparator();
                layout.IndexPage.AddText(
                    "Displayable here, Displayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable hereDisplayable here");
                layout.IndexPage.AddSeparator();
            },
            () => McmMod.ModInfo != null
        );
    }
}