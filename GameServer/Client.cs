using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace GameServer
{
    public class Client
    {
        public Socket ClientSocket;
        private System.AsyncCallback WorkerCallBack;
        public byte[] DataBuffer;

        public Client()
        {
            this.DataBuffer = new byte[512];
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


        }
        #endregion
    }
}
