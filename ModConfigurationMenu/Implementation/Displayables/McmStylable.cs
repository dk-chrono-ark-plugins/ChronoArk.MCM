﻿namespace Mcm.Implementation.Displayables;

#nullable enable

internal class McmStylable(McmStyle? StyleOverride = null) : McmDisplayable, IStylable
{
    private McmStyle _style = StyleOverride == null ? new() : StyleOverride.Copy();
    public McmStyle Style
    {
        get => _style;
        set
        {
            _style = value;
            DeferredUpdate();
        }
    }
}
