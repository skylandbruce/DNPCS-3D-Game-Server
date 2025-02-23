using UtilityDLL.PARSER;

namespace UtilityDLL.ACTIONHANDLER;

public abstract class ActionHandler 
{
    protected Dictionary<string, Action<Parser>> HandleTable {get; set;} = new Dictionary<string, Action<Parser>>();

    public void DoAction(string req, Parser parser){
        if (HandleTable.TryGetValue(req, out Action<Parser>? method))
        {
            method.Invoke(GetParser());
        }
        else
        {
            Console.WriteLine($"No method found for request: {req}");
        }
    }
    public void DoAction(){
        if (HandleTable.TryGetValue(GetParser().Dequeue(), out Action<Parser>? method))
        {
            method.Invoke(GetParser());
        }
        else
        {
            Console.WriteLine($"No method found for request: {GetParser().Dequeue()}");
        }
    }

    public void AddAction(string request, Action<Parser> method)
    {
        if (!HandleTable.ContainsKey(request))
        {
            HandleTable[request] = method;
        }
    }

    protected void RemoveAction(string request)
    {
        if (HandleTable.ContainsKey(request))
        {
            HandleTable.Remove(request);
        }
    }

    public abstract Parser GetParser();
}
