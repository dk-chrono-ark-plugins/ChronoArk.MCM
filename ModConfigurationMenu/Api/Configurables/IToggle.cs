namespace Mcm.Api.Configurables;

public interface IToggle : IConfigurable<bool>
{
    void SetState(bool state);
}
