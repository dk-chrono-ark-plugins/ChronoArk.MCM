﻿namespace Mcm.Api.Displayables;

#nullable enable

/// <summary>
/// Basic button
/// </summary>
public interface IButton : IDisplayable
{
    /// <summary>
    /// Content to display in button rect
    /// </summary>
    IDisplayable Content { get; }

    /// <summary>
    /// Set button's state
    /// </summary>
    bool Interactable { get; set; }

    /// <summary>
    /// On click event handler
    /// </summary>
    Action OnClick { get; }

    /// <summary>
    /// Back layer of button
    /// </summary>
    IImage Background { get; }

    /// <summary>
    /// click the button
    /// </summary>
    void Click();
}
