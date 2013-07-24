using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
namespace GameServer.Handler
{
    public class JoinRoomHandler:IHandler
    {
        public JoinRoomHandler(Client clt)
            : base(clt)
        {

        }

        public override bool Valid(Newtonsoft.Json.Linq.JObject data)
        {
            return true;
        }

        public override void Handler(JObject data)
        {
            int roomid = (int)data[GameKeys.ROOMID];
            Room room = ServerManager.roomList[roomid];
            if (room.totalPlayer == room.maxPlayer)
            {
                GameRequest.sendJoinRoom(this.client, false);
            }
            else
            {
                room.Add(client);
                GameRequest.sendJoinRoom(this.client, true);
                
            }
        }
    }
}
