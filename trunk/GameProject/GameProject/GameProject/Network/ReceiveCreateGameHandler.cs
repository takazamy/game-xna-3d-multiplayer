using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameProject.GameLogic;
using Newtonsoft.Json.Linq;
using XnaGameCore.GameLogic.State;
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
            clt.scrManager.gameManager.room.Add(clt.parentParticipant);
            clt.scrManager.PlayScreen(States.ScreenState.GS_MAIN_GAME);
        }
    }
}
