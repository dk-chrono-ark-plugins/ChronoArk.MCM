using ChronoArkMod.ModData;

namespace Mcm.Common;

public static class McmLoc
{
    public static string I2Loc(this ModInfo? modInfo, string key)
    {
        return modInfo?.localizationInfo.SystemLocalizationUpdate(key) ?? string.Empty;
    }

    internal static class Entry
    {
        public static readonly string Title = McmMod.ModInfo.I2Loc("Mcm/Entry/Title");
        public static readonly string Version = McmMod.ModInfo.I2Loc("Mcm/Entry/Version");
    }

    internal static class Page
    {
        public static readonly string RestartPrompt = McmMod.ModInfo.I2Loc("Mcm/Page/RestartPrompt");
        public static readonly string StubPrompt = McmMod.ModInfo.I2Loc("Mcm/Page/StubPrompt");
        public static readonly string Untitled = McmMod.ModInfo.I2Loc("Mcm/Page/Untitled");
        public static readonly string Back = McmMod.ModInfo.I2Loc("Mcm/Page/Back");
        public static readonly string Apply = McmMod.ModInfo.I2Loc("Mcm/Page/Apply");
        public static readonly string Reset = McmMod.ModInfo.I2Loc("Mcm/Page/Reset");
    }

    internal static class Setting
    {
        public static readonly string Changed = McmMod.ModInfo.I2Loc("Mcm/Setting/Changed");
        public static readonly string Placeholder = McmMod.ModInfo.I2Loc("Mcm/Setting/Placeholder");
        public static readonly string ToggleOn = McmMod.ModInfo.I2Loc("Mcm/Setting/ToggleOn");
        public static readonly string ToggleOff = McmMod.ModInfo.I2Loc("Mcm/Setting/ToggleOff");
        public static readonly string Uninitialized = McmMod.ModInfo.I2Loc("Mcm/Setting/Uninitialized");
    }

    internal static class Index
    {
        public static readonly string AttachDebugScriptRefName =
            McmMod.ModInfo.I2Loc("Mcm/Index/AttachDebugScriptRef/Name");

        public static readonly string AttachDebugScriptRefDesc =
            McmMod.ModInfo.I2Loc("Mcm/Index/AttachDebugScriptRef/Desc");
    }

    internal static class Mockup
    {
        public static readonly string Title = McmMod.ModInfo.I2Loc("Mcm/Mockup/Title");
        public static readonly string Intro = McmMod.ModInfo.I2Loc("Mcm/Mockup/Intro");
        public static readonly string Displayable = McmMod.ModInfo.I2Loc("Mcm/Mockup/IDisplayable");

        public static readonly string ModLayout = McmMod.ModInfo.I2Loc("Mcm/Mockup/ModLayout");
        public static readonly string Usage = McmMod.ModInfo.I2Loc("Mcm/Mockup/ModLayout/Usage");
        public static readonly string Configurable = McmMod.ModInfo.I2Loc("Mcm/Mockup/ModLayout/Configurable");
        public static readonly string McmPage = McmMod.ModInfo.I2Loc("Mcm/Mockup/ModLayout/Page");
        public static readonly string CustomPage = McmMod.ModInfo.I2Loc("Mcm/Mockup/ModLayout/Custom");

        public static readonly string Loc = McmMod.ModInfo.I2Loc("Mcm/Mockup/Loc");
        public static readonly string Text = McmMod.ModInfo.I2Loc("Mcm/Mockup/Text");
        public static readonly string Separator = McmMod.ModInfo.I2Loc("Mcm/Mockup/Text/Separator");
        public static readonly string LoremIpsum = McmMod.ModInfo.I2Loc("Mcm/Mockup/Text/LoremIpsum");
        public static readonly string Image = McmMod.ModInfo.I2Loc("Mcm/Mockup/Image");
        public static readonly string Absens = McmMod.ModInfo.I2Loc("Mcm/Mockup/Image/Missing");
        public static readonly string Composite = McmMod.ModInfo.I2Loc("Mcm/Mockup/IDisplayable/Composite");
        public static readonly string Outro = McmMod.ModInfo.I2Loc("Mcm/Mockup/Outro");
    }
}