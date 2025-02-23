
using UtilityDLL.PARSER;
using UtilityDLL.PARSER.RESPONSE;
using UtilityDLL.PARSER.TODO;
using UtilityDLL.QUEUE.RESPONSE;
using UtilityDLL.QUEUE.TODO;

namespace UtilityDLL.ACTIONHANDLER;

public abstract class TodoHandler : ActionHandler
{
    public TodoHandler(){
        InitHandler();
    }

    private TodoParser parser = new(); 
    public void Parse(TodoEvent eTodo) => parser.Parse(eTodo);

    public override Parser GetParser()
    {
        return parser;
    }
    protected abstract void InitHandler();
}