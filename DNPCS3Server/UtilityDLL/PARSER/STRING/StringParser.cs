

using UtilityDLL.PARSER;

namespace UtilityDLL.PARSER.STRING;

public class StringPaser : Parser
{
    protected Queue<string> RequestQueue {get; set;} = new Queue<string>();

    public override string Dequeue() => RequestQueue.Dequeue();

    public void Parse(string message, char separator)
    {
        string[] requests = message.Split(separator);
        foreach (string req in requests)
        {
            RequestQueue.Enqueue(req);
        }
    }

}
