using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text;
using TCPServerDLL.MONITOR;
using TCPServerDLL.SERVER.BUFFER;
using TCPServerDLL.SERVER.HANDLER;
using TCPServerDLL.TCP;
using UtilityDLL.PARSER;
using UtilityDLL.QUEUE.RESPONSE;
using UtilityDLL.QUEUE.TODO;
using UtilityDLL.UNIQUE;

namespace TCPServerDLL.SERVER;


// Command Parser Interface
public class TcpCommunication : TcpPoolListener
{
    private static CancellationTokenSource clientCTS = new(); // for Common Client Token
    private LinkedList<SMonitor> sMonitors = UniqueLinkedList<SMonitor>.Instance;
    private ResponseQueue responseQueue = new(); // 전송될 버퍼
    private TodoQueue todoQueue;

    private RequestStream requestStream; // 버퍼의 이벤트를 전송
    public required ConcurrentQueue<long> AvailableUsers {private get; init;} 
    
    public TcpCommunication(TodoQueue todoQueue, int port) : base(port)
    {
        sMonitors.AddLast(this);

        this.todoQueue = todoQueue;

        requestStream = new RequestStream(responseQueue){_RequestEncoder = new()};
        requestStream._RequestEncoder.AddAction(ERequest.MESSAGE_BROADCAST.ToString(), MessageBroadcast);
        requestStream._RequestEncoder.AddAction(ERequest.COMMAND_BROADCAST.ToString(), CommandBroadcast);
        sMonitors.AddLast(requestStream);
    }

    private void MessageBroadcast(Parser parser)
    {
        foreach (KeyValuePair<string, TcpDecodableClient> kvp in ClientPool)
        {
            // Message|ID
            string sendMessage = $"{ERequest.MESSAGE_BROADCAST}{ERequest.S}{parser.Dequeue()}{ERequest.S}{parser.Dequeue()}";
            byte[] dataBytes = Encoding.UTF8.GetBytes(sendMessage);
            kvp.Value.WriteByte(dataBytes);
        }
    }

    /// <summary>
    // ResponseStream Command OutputStream Process
    /// </summary>
    /// <param name="parser"> Remained Parser Exicept ERequest </param>
    private void CommandBroadcast(Parser parser)
    {
        foreach (KeyValuePair<string, TcpDecodableClient> kvp in ClientPool)
        {
            // TodoRequest|ID
            string sendCommand = $"{ERequest.COMMAND_BROADCAST}{ERequest.S}{parser.Dequeue()}{ERequest.S}{parser.Dequeue()}";
            byte[] dataBytes = Encoding.UTF8.GetBytes(sendCommand);
            kvp.Value.WriteByte(dataBytes);
        }
    }

    public override TcpClientMonitor GetClient(TcpClient tcpClient)
    {
        var client = new TcpDecodableClient(new BridgeHandler(todoQueue, responseQueue), tcpClient);
        if(AvailableUsers.TryDequeue(out var userID)) client.ID = userID;
        return client;
    }

    public void StartTcpCommunication(){
        StartListener();
        foreach (var monitor in sMonitors)
        {
            monitor.Start();            
        }
    }

    public void StopTcpCommunication(){

        // StopClient();

        // 역순으로 순회 
        foreach (SMonitor monitor in sMonitors.Reverse())
        {
            monitor.Stop();            
        }

        StopListener();
    }
}