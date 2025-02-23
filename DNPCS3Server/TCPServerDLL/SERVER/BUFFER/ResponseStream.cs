using TCPServerDLL.MONITOR;
using UtilityDLL.QUEUE.RESPONSE;

namespace TCPServerDLL.SERVER.BUFFER;

public abstract class ResponseStream : ConditionTaskMonitor<ResponseEvent>
{
    protected ResponseQueue Response { get; set; }

    public ResponseStream(ResponseQueue response){
        this.Response = response;
    }
    protected override bool CheckCodition(out ResponseEvent requestEvent) => Response.TryDequeue(out requestEvent);

    protected override abstract void Routine(in ResponseEvent requestEvent);
}