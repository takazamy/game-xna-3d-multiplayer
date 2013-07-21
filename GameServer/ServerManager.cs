using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;

namespace GameServer
{
    public static class ServerManager
    {
        public static string serverIP;
        public static string DomainName;
        public static string NameOfGameAssociation;
        public static int serverPort;
        public static int maxConnection;
        public static DateTime TimeUpdateQuantity = DateTime.Now;
        public static GameServerMain MainForm = new GameServerMain();
        public static int id = 0;
        public static int rId = 0;

        public static Dictionary<int, Room> roomList = new Dictionary<int,Room>();			//Room List...      
        public static Hashtable socketList = new Hashtable();			//Socket List....
        public static Dictionary<String, Client> ClientList = new Dictionary<String, Client>();

        public static void WriteLogInfoServer(Exception ex, string message)
        {
            MainForm.AppendToRichEditControl("OutputMessage:" + message);
            MainForm.AppendToRichEditControl("MessageError: " + ex.Message);
            MainForm.AppendToRichEditControl("StackTrace: " + ex.StackTrace);
        }
        public static bool CheckExistClient(String endPoint)
        {
            foreach (var item in ClientList)
            {
                if (item.Key == endPoint)
                {
                    return false;   
                }
            }
            return true;
        }
    }
}
