using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GameProject.GameLogic;
using XnaGameCore.GameLogic.State;
using GameProject.Core;
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
            bool success = (bool)data[GameKeys.SUCCESS];
            if (success)
            {
                int idJoin = (int)data[GameKeys.ID];
                MainGame game = (MainGame)clt.scrManager.GetScreensByState(States.ScreenState.GS_MAIN_GAME);
                game.room.CreateParticipant(idJoin);
                clt.scrManager.PlayScreen(States.ScreenState.GS_MAIN_GAME);
            }
            
        }
    }
}
