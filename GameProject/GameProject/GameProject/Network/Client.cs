using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaGameNetworkEngine;
using Microsoft.Xna.Framework;
using GameProject.Core;
using XnaGameCore.GameLogic.State;

namespace GameProject.Network
{
    public class Client:AsyncTcpClient
    {
        ScreenGameManager scrManager;
        public Client(Game game, ScreenGameManager scrManager)
            : base(game)
        {
            this.scrManager = scrManager;
        }

        protected override void OnDataReceived(string data, SocketPacket packet)
        {
            //Case Create Game
            //Gọi Class Create
            base.OnDataReceived(data, packet);
        }

        protected override void OnConnected(SocketPacket server)
        {
            Console.WriteLine("Connected");
            //Tạo game
            scrManager.PlayScreen(States.ScreenState.GS_HOST);
            base.OnConnected(server);
        }
    }
}
