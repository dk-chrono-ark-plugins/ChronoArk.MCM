namespace Mcm.Common;

public static class PageHelper
{
    public static IModLayout GetOwnerLayout(this IPage page)
    {
        return McmManager.GetMcmRegistry(page.Owner)!.Layout;
    }
}