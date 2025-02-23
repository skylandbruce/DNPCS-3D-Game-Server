using System.Collections.Concurrent;
using DNPCS3DLL.NS;
using DNPCS3DLL.PROCESS;
using UtilityDLL;
using UtilityDLL.QUEUE.TODO;

namespace DNPCS3DLL;

public class DNPCS : NervousSystem
{
    private TodoQueue todoQueue;
    // private Define_DNPCS define_DNPCS = new();
    // private NervousSystem? nervousSystem;
    private SignalIO? signalIO;
    private Blood? blood;
    private ANS? ans;

    private Define_DNPCS define_DNPCS;

    

    public DNPCS(TodoQueue _todoQueue, Define_DNPCS _define_DNPCS) : base(_define_DNPCS)
    {
        todoQueue = _todoQueue;
        define_DNPCS = _define_DNPCS;

        // nervousSystem = new NervousSystem(define_DNPCS); 
        // signalIO = new SignalIO(todoQueue, new ReAction(nervousSystem));
        // blood = new Blood(todoQueue, define_DNPCS);
        // ans = new ANS(todoQueue, define_DNPCS);
    }

    public async Task AsyncLoad(){
        
        #region 
        await Task.Delay(5); 
        // nervousSystem = new NervousSystem(define_DNPCS); 
        signalIO = new SignalIO(todoQueue, new ReAction(this), define_DNPCS);
        blood = new Blood(todoQueue, define_DNPCS);
        ans = new ANS(todoQueue, define_DNPCS);
        #endregion

    }

    public bool Ack(){
        Console.WriteLine("DNPC Ack");
        return true;
    }
    
    public void Start(){
        signalIO?.Start();
        // blood?.Start();
        // ans?.Start();
    }

    public void Stop()
    {
        if(signalIO != null)signalIO.Disposed = true;
        if(blood != null)blood.Disposed = true;
        if(ans != null)ans.Disposed = true;
    }

    // ns 가 null 이거나 long == 0 이면 null 처리
    // public ConcurrentQueue<long>? GetAvailableUser() => nervousSystem?.GetAvailableUser();
        
}
