using System.Net;
using System.Net.Sockets;
using TCPServerDLL.MONITOR;

namespace TCPServerDLL.TCP;

public abstract class TcpListenerMonitorAsync : ConditonTaskMonitorAsync<TcpClient> 
{
    private TcpListener tcpListener;

    public TcpListenerMonitorAsync(int port){
        tcpListener = new TcpListener(IPAddress.Any, port);
    }
    protected override async Task<TcpClient> CheckCoditionAsync()
    {        
        try
        {
            return await tcpListener.AcceptTcpClientAsync(CTS.Token);
        }
        catch (OperationCanceledException)
        {
            // 작업 취소됨
            System.Console.WriteLine("!!! Safely Stoped TcpListener !!!");
        } 
        return null!;
    }

    public void StartListener() => tcpListener.Start();
    public void StopListener(){
        tcpListener.Stop();
    }
    
    protected override abstract void Routine(in TcpClient tcpClient);
}