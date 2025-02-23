namespace UtilityDLL.UNIQUE;

public class UniqueLinkedList<T>
{
    private static readonly LinkedList<T> _instance = new LinkedList<T>();

    private UniqueLinkedList() { }

    // LinkedList 인스턴스에 접근할 수 있는 정적 프로퍼티
    public static LinkedList<T> Instance => _instance;
}