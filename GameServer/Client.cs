using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using XnaGameNetworkEngine;
using GameServer.Handler;
using Newtonsoft.Json.Linq;

namespace GameServer
{
    public class Client
    {
        public Socket ClientSocket;
        private System.AsyncCallback WorkerCallBack;
        public byte[] DataBuffer;
        public Participant parentParticipant;
        public Dictionary<string, IHandler> handlerList;
        public Room refRoom;
        public Client()
        {
            this.DataBuffer = new byte[512];
            init(++ServerManager.id);
        }

        public void init(int id)
        {
            parentParticipant = new Participant(id);
            handlerList = new Dictionary<string, IHandler>();
            initHandler();

        }
        public void initHandler()
        {
            handlerList.Add(GameCommand.CREATE_GAME, new CreateGameHandler(this));
            handlerList.Add(GameCommand.GET_LIST_ROOM,new GetListRoomHandler(this));
            handlerList.Add(GameCommand.JOIN_ROOM, new JoinRoomHandler(this));
        }
        #region Wait for data

        public void WaitForData()
        {
            try
            {
                if (!this.ClientSocket.Connected)
                {
                    this.ClientSocket.Close();
                    return;
                }
                if (WorkerCallBack == null)
                {
                    WorkerCallBack = new AsyncCallback(OnDataReceived);
                }

                this.ClientSocket.BeginReceive(this.DataBuffer, 0, 512, SocketFlags.None, WorkerCallBack, this);

            }
            catch (Exception ex)
            {
                ServerManager.WriteLogInfoServer(ex, "WaitForData");
            }
        }

        #endregion
        #region On data receive

        //int iFlag = 0;

        public void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                int iRx = 0;
                if (asyn != null)
                {
                    try
                    {
                        iRx = this.ClientSocket.EndReceive(asyn);

                    }
                    catch
                    {
                        return;
                    }
                }
                else
                    return;
                char[] chars = new char[iRx + 1];

                // Extract the characters as a buffer
                System.Text.Decoder d = Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(this.DataBuffer, 0, iRx, chars, 0);
                
                JObject dataReceive = JObject.Parse(new string(chars));
                IHandler handler = handlerList[(string)dataReceive[GameCommand.COMMAND]];
                if (handler.Valid(dataReceive))
                {
                    handler.Handler(dataReceive);    
                }

                this.ClientSocket.BeginReceive(this.DataBuffer, 0, 512, SocketFlags.None, WorkerCallBack, this);


            }
            catch (Exception ex)
            {
                ServerManager.WriteLogInfoServer(ex, "Server-Client-OnDataReceive:");
            }
        }
        #endregion

        #region SEND
        public void send(string mesg)
        {
            try
            {
                //string _data = String.Format(mesg);
                byte[] _dataBytes = Encoding.ASCII.GetBytes(mesg);
                //ClientSocket.BeginSend(_dataBytes, 0, _dataBytes.Length, SocketFlags.None, new AsyncCallback(OnSendClient), this);
                ClientSocket.Send(_dataBytes);
            }
            catch
            {
            }
        }

        private void OnSendClient(IAsyncResult ar)
        {
            try
            {
                SocketPacket _client = (SocketPacket)ar.AsyncState;
                int _sent = _client.Socket.EndSend(ar);
            }
            catch (ObjectDisposedException ode)
            {
                ServerManager.WriteLogInfoServer(ode, "OnSendClient");
               // WriteError(ode);
            }
            catch (SocketException se)
            {
                ServerManager.WriteLogInfoServer(se, "OnSendClient");
               // WriteError(se);
            }
        }
        #endregion
    }
}
