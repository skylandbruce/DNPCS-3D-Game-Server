
using System.Collections.Concurrent;
using System.Diagnostics;
using DNPCS3DLL;
using MySQLDLL;
using TCPServerDLL;
using UtilityDLL;
using UtilityDLL.QUEUE.TODO;

namespace ManagerForm;

/**
 * Produced by Bruce from Korea
 * Since 2025.02. 24
*/

public partial class FormMain : Form
{
    private TodoQueue todoQueue = new(); 
    // private ReAction reAction = new();

    private DNPCS? ObjDNPCS;
    private MySQL? ObjMySQL;
    private TCPServer? ObjTCPServer;

    private Resource ObjResource = new();

    private ConcurrentQueue<long> availableUsers = new ConcurrentQueue<long>();

    private Define_DNPCS define_DNPCS = new();


    public FormMain()
    {
        InitializeComponent();
        LoadResources();       
    }

    private void FormMain_Load(object sender, EventArgs e){    
     }
    private async void LoadResources()
    {
        UpdateStatus("Loading high-load library...");

        Stopwatch stopwatch= new Stopwatch();
        stopwatch.Start();

        //  Load DNPCS
        Task<DNPCS> taskDNPCS = ObjResource.ReserveDNPCS(todoQueue, define_DNPCS);

        //  Load TCPServer
        Task<TCPServer> taskTCPServer = ObjResource.ReserveTCPServer(todoQueue, 9090);

        //  Load MySQL
        Task<MySQL> taskMySQL = ObjResource.ReserveMySQL("127.0.0.1", "3306", "testdb", "root", "root");

        await Task.WhenAll(taskDNPCS, taskMySQL, taskTCPServer);

        ObjDNPCS = await taskDNPCS;
        
        ObjTCPServer = await taskTCPServer;
        
        ObjMySQL = await taskMySQL;

        ObjTCPServer.AvailableUsers = ObjDNPCS.GetAvailableUser();
        ObjDNPCS.UdpQueue = ObjTCPServer.GetUdpQueue();

        stopwatch.Stop();
        Console.WriteLine($"Total Laptime : {stopwatch.Elapsed}");
        UpdateStatus("Library loaded and ready!!!.");

    } 
    
    private void UpdateStatus(string message)
    {
            if (statusLabel.InvokeRequired)
            {
                statusLabel.Invoke(new Action(() => statusLabel.Text = message));
            }
            else
            {
                statusLabel.Text = message;
            }
    }

    private void ButtonStartStop_Click(object sender, EventArgs e)
    {
        if(buttonStartStop.Text == "Start"){
            buttonStartStop.Text = "Stop";
            if(ObjDNPCS?.Ack() == true) ObjDNPCS?.Start();
            if(ObjTCPServer?.Ack() == true) ObjTCPServer?.StartServer();
            if(ObjMySQL?.Ack() == true) ObjMySQL?.Start();
        }
        else{
            buttonStartStop.Text = "Start";
            
            ObjTCPServer?.StopServer();
            ObjDNPCS?.Stop();
            ObjMySQL?.Stop();
        }
    }
}
