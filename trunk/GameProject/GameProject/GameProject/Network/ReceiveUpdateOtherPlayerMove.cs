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
                    GameKeys.TURRET_STATE_LR stateLR = GameKeys.TURRET_STATE_LR.STAYLR;
                    switch ((int)data[GameKeys.LEFTRIGHTMOVE])
                    {
                        case 1:
                            stateLR = GameKeys.TURRET_STATE_LR.LEFT;
                            break;
                        case 0:
                            stateLR = GameKeys.TURRET_STATE_LR.STAYLR;
                            break;
                        case -1:
                            stateLR = GameKeys.TURRET_STATE_LR.RIGHT;
                            break;
                        
                    }
                    GameKeys.TURRET_STATE_UD stateUD = GameKeys.TURRET_STATE_UD.STAYUD;
                    switch ((int)data[GameKeys.LEFTRIGHTMOVE])
                    {
                        case 1:
                            stateUD = GameKeys.TURRET_STATE_UD.UP;
                            break;
                        case 0:
                            stateUD = GameKeys.TURRET_STATE_UD.STAYUD;
                            break;
                        case -1:
                            stateUD = GameKeys.TURRET_STATE_UD.DOWN;
                            break;
                        
                    }                    
                   
                    item.Value.UpdateTurretMove(stateLR, stateUD);
                }
	        } 
        }
    }
}
