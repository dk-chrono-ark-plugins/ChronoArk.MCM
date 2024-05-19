using ChronoArkMod.Plugin;
using ModConfigurationMenu.Api;

namespace ModConfigurationMenuMock
{
    public class ModConfigurationMenuMock_Plugin : ChronoArkPlugin
    {
        public override void Dispose()
        {

        }

        public override void Initialize()
        {
            var mcm = McmProxy.GetInstance();
            mcm.Register(this);
            mcm.AddText(this, () => "Hello mcm!");
        }
    }
}