using TCPServerDLL.MONITOR;
using TCPServerDLL.REFRESH;
using TCPServerDLL.SERVER;
using TCPServerDLL.TCP;

namespace TCPServerDLL.SERVER.POOL;

public class ClientPoolRefresher : TaskRoutine, IRefresher
{
    private readonly Dictionary<string, TcpClientMonitor> dictionary;

    public ClientPoolRefresher(Dictionary<string, TcpClientMonitor> dictionary){
        this.dictionary = dictionary;
    }

    public void StartRefreshing()
    {
        Start();
    }

    public void StopRefreshing()
    {
        Stop();
    }

    protected override void DoLoop()
    {
        var keysToRemove = dictionary
            .Where(kvp => kvp.Value.CheckOnline() == false)
            .Select(kvp => kvp.Key)
            .ToList();
        foreach (var key in keysToRemove)
        {
            dictionary.Remove(key);
        }
    }
}