using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
namespace GameProject.Network
{
    public class ReceiveJoinRoomHandler:IHandler
    {
        public ReceiveJoinRoomHandler(Client clt)
            :base(clt)
        {

        }

        public override void Handler(JObject data)
        {
            
        }
    }
}
