using System.Net.Sockets;
using TCPServerDLL.MONITOR;
using TCPServerDLL.SERVER.POOL;
using TCPServerDLL.TCP;
using UtilityDLL.UNIQUE;

namespace TCPServerDLL.SERVER;

public abstract class TcpPoolListener : TcpListenerRoutine
{
    private LinkedList<SMonitor> sMonitors = UniqueLinkedList<SMonitor>.Instance;
    protected RefreshableClientPool ClientPool {get; set;} = new RefreshableClientPool(TimeSpan.FromMilliseconds(5000));

    public TcpPoolListener(int port) : base(port){
        sMonitors.AddLast((SMonitor)ClientPool.GetRefresher());
    }

    protected override void Routine(in TcpClient tcpClient)
    {
        var client = GetClient(tcpClient);
        // client.Start();
        ClientPool.AddClient(client, client.ID.ToString());
    }

    public abstract TcpClientMonitor GetClient(TcpClient tcpClient);
}
