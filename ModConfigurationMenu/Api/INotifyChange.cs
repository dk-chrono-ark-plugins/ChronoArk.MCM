namespace Mcm.Api.Configurables;

#nullable enable

public interface INotifyChange
{
    void NotifyChange(object? payload = null);
    void NotifyApply(object? payload = null);
    void NotifyReset(object? payload = null);
}
