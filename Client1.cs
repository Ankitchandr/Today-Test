using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestClient
{
    class Client1
    {
       static void Main(string[] args)
        {
            log4net.Config.BasicConfigurator.Configure();
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(Client1));
            ClientConnection();
        }
        public static void ClientConnection()
        {   
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(Client1));
            try
            {
                while (true)
                {
                    TcpClient tcpclnt = new TcpClient();
                    log.Info("Connecting.....");

                    tcpclnt.Connect("192.168.1.24", 8001);
                    // use the ipaddress as in the server program

                    log.Info("Connected");
                    log.Info("Enter the string to be transmitted : ");

                    String str = Console.ReadLine();
                    Stream stm = tcpclnt.GetStream();

                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(str);
                    log.Info("Transmitting.....");

                    stm.Write(ba, 0, ba.Length);

                    byte[] bb = new byte[100];
                    int k = stm.Read(bb, 0, 100);

                    for (int i = 0; i < k; i++)
                        Console.Write(Convert.ToChar(bb[i]));

                    tcpclnt.Close();
                }
            }

            catch (Exception e)
            {
                log.Error("Error..... " + e.StackTrace);
            }

        }
    }
}
