namespace Mcm.Api.Configurables;

public interface INotifyChanged
{
    void NotifyChange();
    void NotifyApply();
    void NotifyReset();
}
