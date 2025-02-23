namespace TCPServerDLL.UDP;

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;

// 브로드캐스트 데이터 전송을 담당하는 클래스
public class UdpBroadcast
{
    private UdpClient udpClient;
    private int port;
    private int intervals;
    public ConcurrentQueue<byte[]> SendQueue { get; private set; }
    private Thread? sendThread;
    private bool isRunning = false;

    public UdpBroadcast(int port, int intervals)
    {
        this.port = port;
        this.intervals = intervals;
        udpClient = new UdpClient
        {
            EnableBroadcast = true
        };
        SendQueue = new ConcurrentQueue<byte[]>();
        isRunning = true;

    }

    public void Send(byte[] data)
    {
        SendQueue.Enqueue(data);
    }

    private void SendData()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, port);
        while (isRunning)
        {
            // if (sendQueue.TryDequeue(out byte[]? data) && data != null)
            if (SendQueue.TryDequeue(out byte[]? data))
            {
                if (data == null) continue;
                try
                {
                    udpClient.Send(data, data.Length, endPoint);
                    Thread.Sleep(intervals);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error broadcasting message: {ex.Message}");
                }
            }
            else
            {
                Thread.Sleep(intervals);
            }
        }
    }

    public void Start(){
        isRunning = true;
        sendThread = new Thread(SendData);
        sendThread.Start();
    }

    public void Stop()
    {
        isRunning = false;
        sendThread?.Join();
        udpClient.Close();
    }
}

