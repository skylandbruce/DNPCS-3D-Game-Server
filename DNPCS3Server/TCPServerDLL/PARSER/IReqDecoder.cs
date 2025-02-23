namespace TCPServerDLL.REFRESH;

public interface IReqDecoder
{
    public void Decode(string message, char separator, long ID);
}