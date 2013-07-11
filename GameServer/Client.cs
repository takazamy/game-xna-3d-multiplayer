using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using XnaGameNetworkEngine;

namespace GameServer
{
    public class Client
    {
        public Socket ClientSocket;
        private System.AsyncCallback WorkerCallBack;
        public byte[] DataBuffer;
        public Participant parentParticipant;
        public Client()
        {
            this.DataBuffer = new byte[512];            
        }

        public void init(int id)
        {
            parentParticipant = new Participant(id);
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

        int iFlag = 0;

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
               // WriteError(ode);
            }
            catch (SocketException se)
            {
               // WriteError(se);
            }
        }
        #endregion
    }
}
