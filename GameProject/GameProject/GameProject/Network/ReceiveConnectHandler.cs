using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GameProject.GameLogic;
namespace GameProject.Network
{
    public class ReceiveConnectHandler :IHandler
    {
        public ReceiveConnectHandler(Client clt)
            : base(clt)
        {

        }


        public override void Handler(JObject data)
        {
            this.clt.parentParticipant = new Participant((int)data[GameKeys.ID]);
        }
    }
}
