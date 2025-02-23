using UtilityDLL.PARSER;
using UtilityDLL.PARSER.RESPONSE;
using UtilityDLL.QUEUE.RESPONSE;

namespace UtilityDLL.ACTIONHANDLER;

public abstract class ResponseHandler : ActionHandler
{
    public ResponseHandler(){
        InitHandler();
    }

    private ResponseParser parser = new(); 
    public void Parse(ResponseEvent eResponse) => parser.Parse(eResponse);

    public override Parser GetParser()
    {
        return parser;
    }
    protected abstract void InitHandler();
}