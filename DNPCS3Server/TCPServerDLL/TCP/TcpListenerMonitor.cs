using System.Net;
using System.Net.Sockets;
using TCPServerDLL.MONITOR;

namespace TCPServerDLL.TCP;


/// <summary>
/// Deplecated
/// </summary>
public abstract class TcpListenerMonitor : ConditionTaskMonitor<TcpClient> 
{
    private TcpListener tcpListener;

    public TcpListenerMonitor(int port){
        tcpListener = new TcpListener(IPAddress.Any, port);
    }
    protected override bool CheckCodition(out TcpClient tcpClient)
    {        
        try
        {
            tcpClient = tcpListener.AcceptTcpClient();
            // 클라이언트 처리 코드...
            return true;
        }
        catch (OperationCanceledException)
        {
            // 작업 취소됨
            System.Console.WriteLine("!!! Safely Stoped TcpListener !!!");
        } 
        tcpClient = null!;
        return false;
    }

    public void StartListener() => tcpListener.Start();
    public void StopListener(){
        tcpListener.Stop();
    }
    
    protected override abstract void Routine(in TcpClient tcpClient);
}