
using UtilityDLL.PARSER;
using UtilityDLL.PARSER.STRING;

namespace UtilityDLL.ACTIONHANDLER;

public abstract class StreamHandler : ActionHandler
{
    public StreamHandler(){
        InitHandler();
    }

    private StringPaser parser = new(); 
    public void Parse(string stream, char separator) 
        => parser.Parse(stream, separator);

    public override Parser GetParser()
    {
        return parser;
    }
    protected abstract void InitHandler();
}