using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameProject.GameLogic;
using Newtonsoft.Json.Linq;
namespace GameProject.Network
{
    public class ReceiveCreateGameHandler:IHandler
    {
        public ReceiveCreateGameHandler(Client clt)
            : base(clt)
        {

        }


        public override void Handler(JObject data)
        {
            //Load Game

            //
            this.clt.roomId = (int)data[GameKeys.ROOMID];
        }
    }
}
