namespace Mcm.Api.Configurables;

#nullable enable

public interface IConfigurable<T> : IBasicEntry, INotifyChange
{
    T Value { get; }
    Action<T> Save { get; }
    Func<T> Read { get; }
}
