using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UdpServer
{
    private UdpClient udpClient;
    private ConcurrentQueue<byte[]> sendQueue;
    private int intervals;
    private CancellationTokenSource cts;

    public UdpServer(int port, int intervals = 100)
    {
        this.udpClient = new UdpClient(port);
        this.sendQueue = new ConcurrentQueue<byte[]>();
        this.intervals = intervals;
        this.cts = new CancellationTokenSource();

        Task.Run(() => SendDataAsync(cts.Token)); // 데이터 전송 Task 시작
    }

    public void Send(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        sendQueue.Enqueue(data);
    }

    private async Task SendDataAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (sendQueue.TryDequeue(out byte[] data))
            {
                try
                {
                    await udpClient.SendAsync(data, data.Length, "127.0.0.1", ((IPEndPoint)udpClient.Client.LocalEndPoint).Port);
                    await Task.Delay(intervals, token); // 지정된 간격만큼 대기
                }
                catch (Exception ex)
                {
                    // 예외 처리 (로깅, 재전송 등)
                    Console.WriteLine($"Error sending data: {ex.Message}");
                }
            }
            else
            {
                await Task.Delay(intervals, token); // 큐가 비어있으면 잠시 대기
            }
        }
    }

    public void Close()
    {
        cts.Cancel(); // 데이터 전송 Task 중지
        udpClient.Close();
    }
}