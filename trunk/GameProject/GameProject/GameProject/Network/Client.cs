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
using System.Net.Sockets;

namespace GameProject.Network
{
    public class Client:AsyncTcpClient
    {
        public ScreenGameManager scrManager;
        private Dictionary<string,IHandler> handlerList;
        public Participant parentParticipant;
        public int roomId;
        public Client(Game game, ScreenGameManager scrManager)
            : base(game)
        {
            this.scrManager = scrManager;
            handlerList = new Dictionary<string, IHandler>();
            initHandler();
        }

        public void initHandler()
        {
            handlerList.Add(GameCommand.CONNECT, new ReceiveConnectHandler(this, scrManager));
            handlerList.Add(GameCommand.CREATE_GAME, new ReceiveCreateGameHandler(this));
            handlerList.Add(GameCommand.GET_LIST_ROOM, new ReceiveGetListRoomHandler(this, scrManager));
            handlerList.Add(GameCommand.JOIN_ROOM, new ReceiveJoinRoomHandler(this));
        }
        protected override void OnDataReceived(string data, SocketPacket packet)
        {
            //Case Create Game
            //Gọi Class Create
            JObject dataReceive = JObject.Parse(data);
            
            IHandler handler = handlerList[(string)dataReceive[GameCommand.COMMAND]];
            
            handler.Handler(dataReceive);

            WaitForData();
        }

        protected override void OnConnected(SocketPacket server)
        {
            Console.WriteLine("Connected");
            //Tạo game
            
            base.OnConnected(server);
        }

        public void send(string mesg)
        {
            try
            {
                //string _data = String.Format(mesg);
                byte[] _dataBytes = Encoding.ASCII.GetBytes(mesg);
                //ClientSocket.BeginSend(_dataBytes, 0, _dataBytes.Length, SocketFlags.None, new AsyncCallback(OnSendClient), this);
                //this.m_socket.Socket.Send(_dataBytes);
                m_socket.Socket.BeginSend(_dataBytes, 0, _dataBytes.Length, SocketFlags.None, new AsyncCallback(OnSendData), m_socket);
            }
            catch
            {
            }
        }
    }
}
