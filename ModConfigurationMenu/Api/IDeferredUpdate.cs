namespace Mcm.Api;

public interface IDeferredUpdate
{
    bool Deferred { get; }

    void DeferredUpdate();
    void Update();
}
