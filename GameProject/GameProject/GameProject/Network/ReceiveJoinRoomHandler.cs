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
                MainGame game = (MainGame)clt.scrManager.GetScreensByState(States.ScreenState.GS_MAIN_GAME);
                int idJoin = (int)data[GameKeys.ID];
                if (idJoin != clt.parentParticipant.ClientId)
                {
                    game.room.CreateParticipant(idJoin, 2);
                }
                else
                {
                    JArray arr = (JArray)data[GameKeys.INFO];

                    int idCreateRoom = 0;
                    if (game.room == null)
                    {
                        foreach (JObject item in arr)
                        {
                            if ((int)item[GameKeys.POSITION] == 1)
                            {
                                idCreateRoom = (int)item[GameKeys.ID];
                                break;
                            }

                        }
                        game.room = new Room(game.game, this.clt);
                        game.room.CreateParticipant(idCreateRoom, 1);
                        game.room.CreateParticipant(idJoin, 2);
                        game.setMainCamera();

                    }
                    clt.roomId = (int)data[GameKeys.ROOMID];
                    clt.scrManager.PlayScreen(States.ScreenState.GS_MAIN_GAME);
                }
            }
            
        }
    }
}
