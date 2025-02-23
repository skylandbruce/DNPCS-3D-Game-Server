using System.Net.Sockets;
using TCPServerDLL.SERVER.HANDLER;
using UtilityDLL.QUEUE.RESPONSE;

namespace TCPServerDLL.SERVER;

public class TcpDecodableClient : TcpClientRoutine
{
    private BridgeHandler bridegDecoder;
    public TcpDecodableClient(BridgeHandler bridgeHandler, TcpClient tcpClient) : base(tcpClient)
    {
        this.bridegDecoder = bridgeHandler;
    }

    public override bool CheckOnline()
    {
        throw new NotImplementedException();
    }

    protected override void Routine(in string request)
    {
        bridegDecoder.Parse(request, Convert.ToChar(ERequest.S));
        bridegDecoder.DoAction();
    }
}