using System.Net.Sockets;
using TCPServerDLL.TCP;

namespace TCPServerDLL.SERVER;

public class TcpListenerRoutine(int port) : TcpListenerMonitorAsync(port)
{
    protected override void Routine(in TcpClient tcpClient)
    {
        var routine = new TcpClientRoutine(tcpClient);
        routine.Start();
    }
}