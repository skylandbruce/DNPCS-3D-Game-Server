
using System.Diagnostics;
using DNPCS3DLL;
using MySQLDLL;
using TCPServerDLL;
using UtilityDLL.QUEUE.TODO;

namespace ManagerForm;

/**
 * Produced by Bruce from Korea
 * Since 2025.02. 24
*/

class Resource {
    public async Task<DNPCS> ReserveDNPCS(TodoQueue todoQueue, Define_DNPCS define_DNPCS) {
        Stopwatch stopwatch= new Stopwatch();
        stopwatch.Start();

        var varDNPCS = new DNPCS(todoQueue, define_DNPCS);
        await Task.Run(varDNPCS.AsyncLoad);

        stopwatch.Stop();
        Console.WriteLine($"DNPCS loop: {stopwatch.Elapsed}");        

        return varDNPCS;
    }

    public async Task<MySQL> ReserveMySQL(string urlDB, string portDB, string nameDB, string idDB, string passwordDB) {
        Stopwatch stopwatch= new Stopwatch();
        stopwatch.Start();

        var varMySQL = new MySQL(urlDB, portDB, nameDB, idDB, passwordDB);
        await Task.Run(varMySQL.AsyncLoad);
        stopwatch.Stop();
        Console.WriteLine($"MySQL loop: {stopwatch.Elapsed}");        

        return varMySQL;
    }
    
    // public async Task<TCPServer> ReserveTCPServer(TodoQueue todoQueue, int port) {
    public async Task<TCPServer> ReserveTCPServer(TodoQueue todoQueue, int port) {
        Stopwatch stopwatch= new Stopwatch();
        stopwatch.Start();
        
        var varTCPServer = new TCPServer(todoQueue);
        await Task.Run(varTCPServer.LoadAsync);

        stopwatch.Stop();
        Console.WriteLine($"TCPServer loop: {stopwatch.Elapsed}");
        
        return varTCPServer;
    }
}