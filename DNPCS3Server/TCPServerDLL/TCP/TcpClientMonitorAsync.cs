using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TCPServerDLL.MONITOR;

namespace TCPServerDLL.TCP;

public abstract class TcpClientMonitorAsync : ConditonTaskMonitorAsync<string>
{
    protected NetworkStream NwStream;
    private byte[] buffer = new byte[2048];
    
    public TcpClientMonitorAsync(TcpClient tcpClient){
        NwStream = tcpClient.GetStream();
    }

    protected override async Task<string> CheckCoditionAsync()
    {
        int bytesRead = await NwStream.ReadAsync(buffer, 0, buffer.Length);        
        return Encoding.UTF8.GetString(buffer, 0, bytesRead);
    }

    public abstract Task<string> CheckOnline();    
    protected override abstract void Routine(in string request);
}