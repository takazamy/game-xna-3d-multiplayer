using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaGameNetworkEngine;
using Microsoft.Xna.Framework;

namespace GameProject.Network
{
    public class Client:AsyncTcpClient
    {
        public Client(Game game)
            : base(game)
        {
        }

        protected override void OnDataReceived(string data, SocketPacket packet)
        {
            base.OnDataReceived(data, packet);
        }

        protected override void OnConnected(SocketPacket server)
        {
            base.OnConnected(server);
        }
    }
}
