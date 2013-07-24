using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
namespace GameServer.Handler
{
    public class PlayerMoveTurretHandler:IHandler
    {
        public PlayerMoveTurretHandler(Client clt)
            : base(clt)
        {

        }

        public override bool Valid(Newtonsoft.Json.Linq.JObject data)
        {
            return true;
        }

        public override void Handler(JObject data)
        {
            client.refRoom.SendOthersInRoom(data.ToString(),client.parentParticipant.ClientId);
        }
    }
}
