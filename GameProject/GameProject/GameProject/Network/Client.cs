using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaGameNetworkEngine;
using Microsoft.Xna.Framework;
using GameProject.Core;
using XnaGameCore.GameLogic.State;
using Newtonsoft.Json.Linq;
using GameProject.GameLogic;

namespace GameProject.Network
{
    public class Client:AsyncTcpClient
    {
        public ScreenGameManager scrManager;
        private Dictionary<string,IHandler> handlerList;
        public Participant parentParticipant;
        public int roomId;
        public Client(Game game)
            : base(game)
        {           
            handlerList = new Dictionary<string, IHandler>();
            initHandler();
        }

        public void initHandler()
        {
            handlerList.Add(GameCommand.CONNECT, new ReceiveConnectHandler(this));
            handlerList.Add(GameCommand.CREATE_GAME, new ReceiveCreateGameHandler(this));
        }
        protected override void OnDataReceived(string data, SocketPacket packet)
        {
            //Case Create Game
            //Gọi Class Create
            JObject dataReceive = JObject.Parse(data);
            
            IHandler handler = handlerList[(string)dataReceive[GameCommand.COMMAND]];
            handler.Handler(dataReceive);
        }

        protected override void OnConnected(SocketPacket server)
        {
            Console.WriteLine("Connected");
            //Tạo game
            scrManager.PlayScreen(States.ScreenState.GS_HOST);
            base.OnConnected(server);
        }

        public void send(string mesg)
        {
            try
            {
                //string _data = String.Format(mesg);
                byte[] _dataBytes = Encoding.ASCII.GetBytes(mesg);
                //ClientSocket.BeginSend(_dataBytes, 0, _dataBytes.Length, SocketFlags.None, new AsyncCallback(OnSendClient), this);
                this.m_socket.Socket.Send(_dataBytes);
            }
            catch
            {
            }
        }
    }
}
