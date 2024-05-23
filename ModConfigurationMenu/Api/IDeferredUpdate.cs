namespace Mcm.Api;

#nullable enable

public interface IDeferredUpdate
{
    bool Deferred { get; }

    void DeferredUpdate();
    void Update();
}
