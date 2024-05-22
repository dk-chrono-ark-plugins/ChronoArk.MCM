namespace Mcm.Api;

public interface IDeferredUpdater
{
    bool Deferred { get; }

    void DeferredUpdate();
}
