using System.Net.Sockets;
using System.Text;
using TCPServerDLL.TCP;

namespace TCPServerDLL.SERVER;

public class TcpClientRoutine : TcpClientMonitor
{
    private TcpClient tcpClient;

    public TcpClientRoutine(TcpClient tcpClient) : base(tcpClient){
        this.tcpClient = tcpClient;
        
    }

    protected override void Routine(in string request)
    {
        byte[] data = Encoding.UTF8.GetBytes( request );
        WriteByte(data);
    }

    public void WriteByte(byte[] response){
        Task task = Task.Run(() => NwStream.WriteAsync(response, 0, response.Length));
    }

    public override bool CheckOnline()
    {
        return tcpClient.Connected;
    }
}