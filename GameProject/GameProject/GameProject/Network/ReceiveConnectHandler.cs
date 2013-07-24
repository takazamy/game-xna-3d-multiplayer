using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GameProject.GameLogic;
using GameProject.Core;
using XnaGameCore.GameLogic.State;
namespace GameProject.Network
{
    public class ReceiveConnectHandler :IHandler
    {
        ScreenGameManager scrManager;
        public ReceiveConnectHandler(Client clt, ScreenGameManager scrManager)
            : base(clt)
        {
            this.scrManager = scrManager;
        }


        public override void Handler(JObject data)
        {
            this.clt.parentParticipant = new Participant((int)data[GameKeys.ID], scrManager.game);
            scrManager.PlayScreen(States.ScreenState.GS_HOST);
        }
    }
}
