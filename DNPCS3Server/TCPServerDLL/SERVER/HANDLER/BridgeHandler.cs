
using System.Security.Cryptography;
using UtilityDLL.ACTIONHANDLER;
using UtilityDLL.PARSER;
using UtilityDLL.QUEUE.RESPONSE;
using UtilityDLL.QUEUE.TODO;

namespace TCPServerDLL.SERVER.HANDLER;

/// <summary>
/// string message Parsing :
/// Parse(string message, char seperator)
/// string message Execute (Inbound or Outbound) : 
/// Decode()
/// </summary>
public class BridgeHandler : StreamHandler
{
    private TodoQueue todoQueue;
    private ResponseQueue responseQueue;
    public BridgeHandler(TodoQueue todoQueue, ResponseQueue responseQueue){
        this.todoQueue = todoQueue;
        this.responseQueue = responseQueue;
    }

    protected override void InitHandler()
    {
        AddAction(ERequest.COMMAND_BROADCAST.ToString(), CommandBroadcast);
        AddAction(ERequest.COMMAND_PRIVATE.ToString(), CommandPrivate);
        AddAction(ERequest.MESSAGE_BROADCAST.ToString(), MessageBroadcast);
        AddAction(ERequest.MESSAGE_PRIVATE.ToString(), MessagePrivate);
    }

    protected void CommandBroadcast(Parser parser)
    {
        // Req|ID
        ETodoRequest todoRequest = (ETodoRequest)Enum.Parse(typeof(ETodoRequest), parser.Dequeue());
        string id = parser.Dequeue();
        TodoEvent todoEvent = 
            new TodoEvent(){Target=ETodoTarget.USER, Req=todoRequest, IDorLevel = id};
        todoQueue.Enqueue(todoEvent);

        // Request|TodoRequest|ID
        ResponseEvent responseEvent = new ResponseEvent(){
            Request=ERequest.COMMAND_BROADCAST, 
            TodoRequest=todoRequest, 
            ID=id
        };
        responseQueue.Enqueue(responseEvent);
    }

    protected void CommandPrivate(Parser parser)
    {
    }


    protected void MessageBroadcast(Parser parser)
    {
        // Message|ID
        string message = parser.Dequeue();
        string id = parser.Dequeue();

        // Request|Message|ID
        ResponseEvent responseEvent= new ResponseEvent(){
            Request=ERequest.MESSAGE_BROADCAST, 
            Message=message, 
            ID=id
        };
        responseQueue.Enqueue(responseEvent);
    }

    protected void MessagePrivate(Parser parser)
    {
        // Message|IDTarget|ID
        string message = parser.Dequeue();
        string idTarget = parser.Dequeue();
        string id = parser.Dequeue();

        // Request|Message|IDTarget|ID
        ResponseEvent responseEvent= new ResponseEvent(){
            Request=ERequest.MESSAGE_PRIVATE, 
            Message=message, 
            IDTarget=idTarget,
            ID=id
        };
        responseQueue.Enqueue(responseEvent);
    }
}