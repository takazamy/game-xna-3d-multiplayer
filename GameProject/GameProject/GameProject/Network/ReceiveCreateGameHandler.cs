using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameProject.GameLogic;
using Newtonsoft.Json.Linq;
using XnaGameCore.GameLogic.State;
using GameProject.Core;
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
            Console.WriteLine("Tao Phong");
            //
            this.clt.roomId = (int)data[GameKeys.ROOMID];

            
            MainGame game = (MainGame)clt.scrManager.GetScreensByState(States.ScreenState.GS_MAIN_GAME);
            game.room = new Room(game.game, this.clt);
            game.room.CreateParticipant(clt.parentParticipant.ClientId, 1);
            game.setMainCamera();

            clt.scrManager.PlayScreen(States.ScreenState.GS_MAIN_GAME);
            
        }
    }
}
