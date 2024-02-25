using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

namespace Artemis.Plugins.AuroraxArtemis;

public class AuroraInterface
{
    private readonly byte[] _end = "\n"u8.ToArray();
    
    public async Task ShutdownAuroraDevices()
    {
        var sendCommand = SendCommand("quitDevices");
        
        await Task.WhenAny(Task.Delay(2), sendCommand);
    }
    
    public async Task StartAuroraDevices()
    {
        var sendCommand = SendCommand("startDevices");

        await Task.WhenAny(Task.Delay(2), sendCommand);
    }

    private Task SendCommand(string command)
    {
        return SendCommand(Encoding.UTF8.GetBytes(command));
    }

    private async Task SendCommand(byte[] command)
    {
        var client = new NamedPipeClientStream(".", "aurora\\interface", PipeDirection.Out, PipeOptions.Asynchronous);
        await client.ConnectAsync(2000);
        if (!client.IsConnected)
            return;
        
        client.Write(command, 0, command.Length);
        client.Write(_end, 0, _end.Length);
        
        client.Flush();
        client.Close();
    }
}