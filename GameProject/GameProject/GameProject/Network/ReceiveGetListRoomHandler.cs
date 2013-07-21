using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GameProject.Core;
using XnaGameCore.GameLogic.Screens;
using XnaGameCore.GameLogic.State;
using GameProject.GameLogic;
namespace GameProject.Network
{
    public class ReceiveGetListRoomHandler:IHandler
    {
        ScreenGameManager scrManager;
        public ReceiveGetListRoomHandler(Client clt, ScreenGameManager scrManager)
            : base(clt)
        {
            this.scrManager = scrManager;
        }

        public override void Handler(JObject data)
        {
            scrManager.PlayScreen(States.ScreenState.GS_JOIN);
            Join joinScreen = (Join)scrManager.GetScreensByState(States.ScreenState.GS_JOIN);
            joinScreen.ListRoomData = (JArray)data[GameKeys.ROOM_LIST];
        }
    }
}
