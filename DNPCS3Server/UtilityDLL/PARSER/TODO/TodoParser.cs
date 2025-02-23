
using UtilityDLL.QUEUE.TODO;

namespace UtilityDLL.PARSER.TODO;

public class TodoParser : Parser
{
    protected Queue<string> _Queue {get; set;} = new Queue<string>();

    public override string Dequeue() => _Queue.Dequeue();

    public void Parse(TodoEvent todoEvent)
    {
        _Queue.Enqueue(todoEvent.Req.ToString());
        _Queue.Enqueue(todoEvent.Target.ToString());
        _Queue.Enqueue(todoEvent.IDorLevel.ToString());
    }    
}