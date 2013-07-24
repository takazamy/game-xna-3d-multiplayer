using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer
{
    public class Room
    {
        public int roomId = 0;
        public Dictionary<int, Client> clientList;
        public int totalPlayer = 0;
        
        public int maxPlayer = 2;
        public Room(int id)
        {
            roomId = id;
            clientList = new Dictionary<int, Client>();
        }

        public void Add(Client clt)
        {
            if (totalPlayer == 0)
            {
                clt.parentParticipant.position = 1;
            }
            else
            {
                clt.parentParticipant.position = 2;
            }
            clientList.Add(clt.parentParticipant.ClientId, clt);
            totalPlayer++;
            clt.refRoom = this;
        }

        public void SendAllInRoom(String mesg)
        {
            foreach (var item in clientList)
            {
                item.Value.send(mesg);
            }
        }

        public void SendOthersInRoom(String mesg, int myId)
        {
            foreach (var item in clientList)
            {
                if (item.Key != myId)
                {
                    item.Value.send(mesg);
                }
            }
        }
    }
}
