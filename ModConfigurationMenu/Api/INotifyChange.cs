namespace Mcm.Api.Configurables;

public interface INotifyChange
{
    void NotifyChange();
    void NotifyApply();
    void NotifyReset();
}
