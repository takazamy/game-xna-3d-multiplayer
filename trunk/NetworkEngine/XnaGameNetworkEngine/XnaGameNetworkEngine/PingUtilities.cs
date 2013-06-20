using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace XnaGameNetworkEngine
{
    public class PingUtilities
    {
        static CountdownEvent countdown;
        static int upCount = 0;
        static object lockObj = new object();
        const bool resolveNames = true;
        public static List<string> listHost =new List<string>();
       
        public static bool CheckPing(int port)
        {
            bool resultCheck = false;
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            String myAddress = ipAddress.ToString();
            String address = ipAddress.ToString();
            String[] temp = address.Split('.');
            bool connectResult = false;
            
            address = "";
            int i;
            try
            {
                for (i = 0; i < temp.Length - 1; i++)
                {
                    address += temp[i] + ".";
                }

                string ipBase = address;
                i = 2;
                while (i < 255)
                {
                    Socket ScanIpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    string ip = ipBase + i.ToString();
                    IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ip), port);
                    if (ip == myAddress)
                    {
                        continue;
                    }


                    IAsyncResult result = ScanIpSocket.BeginConnect(ipEnd, null, null);
                    connectResult = result.AsyncWaitHandle.WaitOne(10, true);

                    if (connectResult)
                    {
                        resultCheck = true;
                        listHost.Add(ip);
                    }
                    ScanIpSocket = null;
                    i++;
                }
            }
            catch
            {
                return false;
            }

            return resultCheck;      
           
        }

       

       
    }
}
