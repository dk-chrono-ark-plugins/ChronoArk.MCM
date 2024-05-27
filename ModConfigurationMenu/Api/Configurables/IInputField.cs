using TMPro;

namespace Mcm.Api.Configurables;

public interface IInputField : IConfigurable<string>
{
    /// <summary>
    ///     Input charset validator
    /// </summary>
    TMP_InputField.CharacterValidation CharacterValidation { get; }
}