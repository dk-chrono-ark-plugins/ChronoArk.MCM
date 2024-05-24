namespace Mcm.Api.Configurables;

public interface IConfigurable<T> : IBasicEntry, INotifyChange
{
    T Value { get; }
    Action<T> Save { get; }
    Func<T> Read { get; }

    void SetValue(T value);
}