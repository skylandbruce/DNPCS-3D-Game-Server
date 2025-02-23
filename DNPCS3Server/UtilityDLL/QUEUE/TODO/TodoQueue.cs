using System.Collections.Concurrent;

namespace UtilityDLL.QUEUE.TODO;

public class TodoQueue : EventQueue
{
    private ConcurrentQueue<TodoEvent> queue = new ConcurrentQueue<TodoEvent>();

    public void Enqueue(TodoEvent item)
    {
        queue.Enqueue(item);
    }

    public bool TryDequeue(out TodoEvent item)
    {
        return queue.TryDequeue(out item);
    }

    public int Count => queue.Count;
}
