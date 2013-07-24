using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GameProject.Core;
using XnaGameCore.GameLogic.State;
using GameProject.GameLogic;
namespace GameProject.Network
{
    public class ReceiveUpdateOtherPlayerMove:IHandler
    {
        public ReceiveUpdateOtherPlayerMove(Client clt)
            : base(clt)
        {

        }

        public override void Handler(JObject data)
        {
            MainGame game = (MainGame)clt.scrManager.GetScreensByState(States.ScreenState.GS_MAIN_GAME);
            foreach (var item in game.room.clientList)
	        {
                if (!item.Value.isMe)
                {
                    GameKeys.TURRET_STATE_LR stateLR = (GameKeys.TURRET_STATE_LR)(int)data[GameKeys.LEFTRIGHTMOVE];
                    GameKeys.TURRET_STATE_UD stateUD = (GameKeys.TURRET_STATE_UD)(int)data[GameKeys.UPDOWNMOVE];
                    item.Value.UpdateTurretMove(stateLR, stateUD);
                }
	        } 
        }
    }
}
