using System.Collections;
using System.Collections.Concurrent;
using TCPServerDLL.REFRESH;
using TCPServerDLL.TCP;

namespace TCPServerDLL.SERVER.POOL;

public class RefreshableClientPool : IRefreshable, IEnumerable
{
    private readonly Dictionary<string, TcpClientMonitor> dictionary = new();
    private readonly TimeSpan interval;
    // public ConcurrentQueue<long>? AvailableKey;

    public RefreshableClientPool(TimeSpan interval)
    {
        this.interval = interval;
    }

    public void AddClient(TcpClientMonitor client, string key)
    {
        lock (dictionary)
        {
            dictionary[key] = client;
        }
    }
    // public void AddClient(ClientRoutine client)
    // {
    //     lock (dictionary)
    //     {
    //         if (AvailableKey == null) return;
    //         if(AvailableKey.TryDequeue(out long key)){
    //             dictionary[key.ToString()] = client;
    //         }
    //     }
    // }

    public void RemoveClient(string key)
    {
        lock (dictionary)
        {
            dictionary.Remove(key);
        }
    }

    public IRefresher GetRefresher()
    {
        return new ClientPoolRefresher(dictionary){Intervals = interval};
    }

    public IEnumerator GetEnumerator()
    {
        return dictionary.GetEnumerator();
    }
}
