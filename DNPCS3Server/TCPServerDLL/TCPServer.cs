using UtilityDLL;
using System.Collections.Concurrent;
using TCPServerDLL.UDP;
using TCPServerDLL.SERVER;
using TCPServerDLL.MONITOR;
using UtilityDLL.UNIQUE;
using UtilityDLL.QUEUE.TODO;

namespace TCPServerDLL;

public class TCPServer 
{
    private TcpCommunication tcpCommunication;
    private UdpBroadcast udpBroadcast;
    public ConcurrentQueue<long> AvailableUsers {private get; set;} = default!;

    public TCPServer(TodoQueue _todoQueue){
        tcpCommunication = new(_todoQueue, 9090){AvailableUsers = AvailableUsers};
        udpBroadcast = new(9091, 200);
    }

     public async Task LoadAsync(){

        #region 
        await Task.Delay(5);
        #endregion
        
    }

    public bool Ack(){
        Console.WriteLine("TCPServer Ack");
        return true;
    }

    public ConcurrentQueue<byte[]> GetUdpQueue(){
        return udpBroadcast.SendQueue;
    }

    public void StartServer(){
        tcpCommunication.StartTcpCommunication();
        udpBroadcast.Start();

    }

    public void StopServer(){
        tcpCommunication.StopTcpCommunication();
        udpBroadcast.Stop();
    }
}
