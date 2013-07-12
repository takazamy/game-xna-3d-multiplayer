using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
namespace GameServer.Handler
{
    public class CreateGameHandler:IHandler
    {
        public CreateGameHandler(Client clt)
            : base(clt)
        {
        }

        public override void Handler(JObject data)
        {
            Room room = new Room(ServerManager.rId++);
            room.Add(this.client);
            this.client.refRoom = room;
            ServerManager.roomList.Add(room.roomId,room);
            GameRequest.sendCreateRoom(this.client);
        }
    }
}
