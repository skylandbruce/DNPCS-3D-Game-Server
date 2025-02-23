using UtilityDLL.QUEUE.RESPONSE;

namespace TCPServerDLL.REFRESH;

public interface IEventEncoder
{
    public void Parse(ResponseEvent eRequest);
    public void Encode();
}