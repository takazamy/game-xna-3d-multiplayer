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
        private int totalPlayer = 0;
        
        public int maxPlayer;
        public Room(int id)
        {
            roomId = id;
            clientList = new Dictionary<int, Client>();
        }

        public void Add(Client clt)
        {
            clientList.Add(clt.parentParticipant.ClientId, clt);
            totalPlayer++;
        }
    }
}
