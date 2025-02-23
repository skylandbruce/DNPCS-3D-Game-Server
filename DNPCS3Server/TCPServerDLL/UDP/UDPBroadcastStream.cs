using System.Text;

namespace TCPServerDLL.UDP;



public class UDPBroadcastStream :UdpBroadcast
{
    // private UdpBroadcast base;

    public UDPBroadcastStream(int port, int intervals = 100) : base(port, intervals)
    {
    }

    public void Broadcast(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        base.Send(data);
    }

    public void Broadcast(byte[] data)
    {
        base.Send(data);
    }
    
}

// 사용 예시
// public class Example
// {
//     public static void Main(string[] args)
//     {
//         UDPBroadcaster broadcaster = new UDPBroadcaster(5000); // 5000번 포트로 브로드캐스터 생성

//         broadcaster.Broadcast("Hello, everyone!"); // 문자열 브로드캐스팅

//         byte[] customData = { 0x01, 0x02, 0x03 }; // 바이트 배열 브로드캐스팅
//         broadcaster.Broadcast(customData);


//         broadcaster.Close();
//     }
// }