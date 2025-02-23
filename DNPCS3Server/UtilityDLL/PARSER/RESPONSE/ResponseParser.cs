
using UtilityDLL.QUEUE.RESPONSE;

namespace UtilityDLL.PARSER.RESPONSE;

public class ResponseParser : Parser
{
    protected Queue<string> _Queue {get; set;} = new Queue<string>();

    public override string Dequeue() => _Queue.Dequeue();

    public void Parse(ResponseEvent responseEvent)
    {
        _Queue.Enqueue(responseEvent.TodoRequest.ToString());
        _Queue.Enqueue(responseEvent.Message);
        _Queue.Enqueue(responseEvent.ID.ToString());
    }    
}