namespace Mcm.Api.Configurables;

public interface IInputField : IConfigurable<string>
{
    /// <summary>
    ///     Current string input
    /// </summary>
    string CurrentInput { get; }

    /// <summary>
    ///     Used to determine input validity<br />
    ///     Default set by Mcm
    /// </summary>
    Func<string, bool> InputPredicate { get; set; }
}