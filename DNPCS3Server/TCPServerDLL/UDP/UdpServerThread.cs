namespace TCPServerDLL.UDP;

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UdpServerThread
{
    private UdpClient udpClient;
    private ConcurrentQueue<byte[]> sendQueue;
    private int intervals;
    private Thread sendThread;
    private bool isRunning;

    public UdpServerThread(int port, int intervals = 100)
    {
        this.udpClient = new UdpClient(port);
        this.sendQueue = new ConcurrentQueue<byte[]>();
        this.intervals = intervals;
        this.isRunning = true;

        sendThread = new Thread(SendData);
        sendThread.Start();
    }

    public void Send(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        sendQueue.Enqueue(data);
    }

    private void SendData()
    {
        while (isRunning)
        {
            if (sendQueue.TryDequeue(out byte[] data))
            {
                try
                {
                    udpClient.Send(data, data.Length, "127.0.0.1", ((IPEndPoint)udpClient.Client.LocalEndPoint).Port);
                    Thread.Sleep(intervals); // 지정된 간격만큼 대기
                }
                catch (Exception ex)
                {
                    // 예외 처리 (로깅, 재전송 등)
                    Console.WriteLine($"Error sending data: {ex.Message}");
                }
            }
            else
            {
                Thread.Sleep(intervals); // 큐가 비어있으면 잠시 대기
            }
        }
    }

    public void Close()
    {
        isRunning = false; // 스레드 종료 플래그 설정
        sendThread.Join(); // 스레드 종료 대기
        udpClient.Close();
    }
}