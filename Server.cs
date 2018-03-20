using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleAppTest
{
    class Server
    {
        public static void Main(string[] args)
        {
            log4net.Config.BasicConfigurator.Configure();
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(Server));
            ServerConnection();
        }
      public static void ServerConnection()
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(Server));

         try
            {
                while (true)
                {
                    IPAddress ipAd = IPAddress.Parse("192.168.1.24");
                    TcpListener myList = new TcpListener(ipAd, 8001);
                    myList.Start();
                    log.Info("The server is running at port 8001...");
                    log.Info("The local End point is  :" +
                    myList.LocalEndpoint);
                    Console.WriteLine("Waiting for a connection.....");
                    Socket s = myList.AcceptSocket();
                    log.Info("Connection accepted from " + s.RemoteEndPoint);
                    byte[] b = new byte[100];
                    int k = s.Receive(b);
                    log.Info("Recieved...");
                    string Command = string.Empty;
                    for (int i = 0; i < k; i++)
                    {
                        Command = Command + Convert.ToChar(b[i]);
                    }
                    Console.WriteLine(Command);
                    ASCIIEncoding asen = new ASCIIEncoding();
                    s.Send(asen.GetBytes("The string was recieved by the server."));
                    Console.WriteLine("\nSent Acknowledgement");
                    s.Close();
                    myList.Stop();
                }
            }
            catch (Exception e)
            {
             
                log.Error("Error..... " + e.StackTrace);
            }
        }

        
    }
}
