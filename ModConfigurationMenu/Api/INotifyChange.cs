﻿namespace Mcm.Api;

/// <summary>
///     Notify changes to implementers, without a center broadcaster
/// </summary>
public interface INotifyChange
{
    /// <summary>
    ///     Send out changed event
    /// </summary>
    void NotifyChange(object? payload = null);

    /// <summary>
    ///     Send out applied event
    /// </summary>
    /// <param name="payload"></param>
    void NotifyApply(object? payload = null);

    /// <summary>
    ///     Send out reset event
    /// </summary>
    /// <param name="payload"></param>
    void NotifyReset(object? payload = null);
}