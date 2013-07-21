using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GameProject.GameLogic;
using XnaGameCore.GameLogic.State;
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
            //this.clt.roomId = (int)data[GameKeys.ROOMID];
            //clt.scrManager.gameManager.room.Add(clt.parentParticipant);
            clt.scrManager.PlayScreen(States.ScreenState.GS_MAIN_GAME);
        }
    }
}
