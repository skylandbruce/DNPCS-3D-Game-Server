using System.Collections.Concurrent;
using UtilityDLL;

namespace UtilityDLL.QUEUE.RESPONSE;

public class ResponseQueue : EventQueue
{
    private ConcurrentQueue<ResponseEvent> queue = new ConcurrentQueue<ResponseEvent>();

    public void Enqueue(ResponseEvent item)
    {
        queue.Enqueue(item);
    }

    public bool TryDequeue(out ResponseEvent item) => queue.TryDequeue(out item);

    public int Count => queue.Count;
}
