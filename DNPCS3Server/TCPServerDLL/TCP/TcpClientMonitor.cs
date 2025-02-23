using System.Net;
using System.Net.Sockets;
using System.Text;
using TCPServerDLL.MONITOR;

namespace TCPServerDLL.TCP;

public abstract class TcpClientMonitor : ConditionTaskMonitor<string>
{
    // protected TcpClient Client {get; set;}
    // protected NetworkStream StreamClient {get; set;}
    // protected byte[] BufferStream = new byte[2048];
    protected NetworkStream NwStream;
    private byte[] buffer = new byte[2048];
    
    public TcpClientMonitor(TcpClient tcpClient){
        NwStream = tcpClient.GetStream();
    }

    protected override bool CheckCodition(out string request)
    {
        int bytesRead = NwStream.Read(buffer, 0, buffer.Length);        
        request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        if (bytesRead == 0) return false;
        return true;
    }

    public abstract bool CheckOnline();    
    protected override abstract void Routine(in string request);
}