using Artemis.Core;

namespace Artemis.Plugins.AuroraxArtemis;

public class Bootstrapper : PluginBootstrapper
{
    private readonly AuroraInterface _auroraInterface = new();
    
    public override void OnPluginLoaded(Plugin plugin)
    {
        //unused
    }

    public override void OnPluginEnabled(Plugin plugin)
    {
        _auroraInterface.ShutdownAuroraDevices().Wait();
    }
    
    public override void OnPluginDisabled(Plugin plugin)
    {
        _auroraInterface.StartAuroraDevices().Wait();
    }
}
