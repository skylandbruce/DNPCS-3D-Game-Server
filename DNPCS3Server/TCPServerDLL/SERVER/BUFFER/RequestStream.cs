using TCPServerDLL.SERVER.HANDLER;
using UtilityDLL.QUEUE.RESPONSE;

namespace TCPServerDLL.SERVER.BUFFER;

public class RequestStream : ResponseStream
{
    public required RequestEncoder _RequestEncoder { get; set; }
         
    public RequestStream(ResponseQueue response) : base(response)
    {
    }

    protected override void Routine(in ResponseEvent responseEvent)
    {
        _RequestEncoder.Parse(responseEvent);
        _RequestEncoder.DoAction();
    }
}