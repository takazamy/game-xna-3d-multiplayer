using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Net.Sockets;
using System.Net;
using System.Text;


namespace XnaGameNetworkEngine
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class AsyncTcpServer : Microsoft.Xna.Framework.GameComponent
    {
        #region Members
        private List<SocketPacket> m_clients = new List<SocketPacket>();
        private AsyncCallback m_workerCallback;

        protected Socket m_listener;

        protected string m_ip = "127.0.0.1";
        protected int m_port = 11000;

        private int m_maxConnections = 10;

        private string m_name = String.Empty;

        //protected IReporterService m_reporter;

        #endregion

        #region Events
        public event DataReceivedHandler DataReceived;

        public event ClientConnectedHandler ClientConnected;
        public event ClientDisconnectedHandler ClientDisconnected;

        public event ClientAuthRequestHandler ClientAuthRequest;

        public event ServerStartedHandler ServerStarted;
        public event ServerStoppedHandler ServerStopped;
        #endregion

        #region Constructor
        public AsyncTcpServer(Game game)
            : base(game)
        {
            m_ip = GetHostIP();
            SetupHandlers(game);
        }

        public AsyncTcpServer(string serverName, Game game)
            : base(game)
        {
            m_name = serverName;
            m_ip = GetHostIP();
            SetupHandlers(game);
        }

        ~AsyncTcpServer()
        {
            Dispose(true);
        }

        protected string GetHostIP()
        {
            string myHost = string.Empty;
            string hostIP = string.Empty;
            try
            {
                myHost = System.Net.Dns.GetHostName();
                hostIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();
            }
            catch (Exception e)
            {
                WriteError(e);
            }
            return hostIP;
        }

        protected void SetupHandlers(Game game)
        {
            #region Add Handlers
            this.DataReceived += new DataReceivedHandler(OnDataReceived);
            this.ClientConnected += new ClientConnectedHandler(OnClientConnected);
            this.ClientDisconnected += new ClientDisconnectedHandler(OnClientDisconnected);
            this.ClientAuthRequest += new ClientAuthRequestHandler(OnClientAuthRequest);
            this.ServerStarted += new ServerStartedHandler(OnServerStarted);
            this.ServerStopped += new ServerStoppedHandler(OnServerStopped);
            #endregion

            game.Exiting += new EventHandler<EventArgs>(OnGameExiting);
        }
        #endregion

        #region Virtual Event Handlers
        protected virtual void OnClientAuthRequest(SocketPacket packet)
        {
        }

        protected virtual void OnClientDisconnected(SocketPacket packet)
        {
        }

        protected virtual void OnClientConnected(SocketPacket packet)
        {
        }

        protected virtual void OnDataReceived(string data, SocketPacket packet)
        {
        }

        protected virtual void OnServerStopped()
        {
        }

        protected virtual void OnServerStarted()
        {
        }
        #endregion

        #region Starting and Stopping
        public void Start(int port)
        {
            m_port = port;
            Start();
        }

        public void Start()
        {
            try
            {
                if (m_listener != null)
                    return;
                m_listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint ipLocal = new IPEndPoint(ipAddress, m_port);
                m_listener.Bind(ipLocal);
                m_listener.Listen(m_maxConnections);
                m_listener.BeginAccept(new AsyncCallback(OnClientConnect), null);

                if (ServerStarted != null)
                    ServerStarted.Invoke();
            }
            catch (SocketException se)
            {
                WriteError(se);
            }
        }

        public void Stop()
        {
            if (m_listener != null)
            {
                m_listener.Close();
            }
            if (m_clients != null)
            {
                foreach (SocketPacket _client in m_clients)
                {
                    if (_client.Socket.Connected)
                        if (ClientDisconnected != null)
                            ClientDisconnected.Invoke(_client);

                    _client.Socket.Close();
                }

                m_clients.Clear();
                if (ServerStopped != null)
                    ServerStopped.Invoke();
            }
        }
        #endregion

        #region Callbacks and and Methods for Client Handling
        protected virtual void OnClientConnect(IAsyncResult ar)
        {
            try
            {
                int _tempID = m_clients.Count + 1;
                SocketPacket _tempClient = new SocketPacket(_tempID.ToString());

                _tempClient.Socket = m_listener.EndAccept(ar);

                m_clients.Add(_tempClient);

                string _str = String.Format("Client #{0} connected.", _tempID);

                if (ClientConnected != null)
                    ClientConnected.Invoke(_tempClient);

                if (DataReceived != null)
                    DataReceived.Invoke(_str, _tempClient);

                WaitForData(_tempClient);

                m_listener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException ode)
            {
                WriteError(ode, "OnClientConnect: Socket has been closed.");
            }
            catch (SocketException se)
            {
                WriteError(se);
            }
        }

        protected void WaitForData(SocketPacket client)
        {
            try
            {
                if (m_workerCallback == null)
                    m_workerCallback = new AsyncCallback(OnClientDataReceived);

                client.Socket.BeginReceive(client.DataBuffer, 0, client.BufferLength,
                                           SocketFlags.None, m_workerCallback, client);
            }
            catch (SocketException se)
            {
                WriteError(se);
            }
        }

        protected virtual void OnClientDataReceived(IAsyncResult ar)
        {
            SocketPacket _client = (SocketPacket)ar.AsyncState;

            try
            {
                int iRx = 0;
                iRx = _client.Socket.EndReceive(ar);

                if (iRx <= 0)   // If nothing was read return
                    return;

                string _strData = Encoding.ASCII.GetString(_client.DataBuffer, 0, iRx);
                if (DataReceived != null)
                    DataReceived.Invoke(_strData, _client);

                _client.ResetBuffer();

                WaitForData(_client);
            }
            catch (ObjectDisposedException ode)
            {
                WriteError(ode, "OnClientDataReceived: Socket has been closed.");

                if (this.ClientDisconnected != null)
                    ClientDisconnected.Invoke(_client);
            }
            catch (SocketException se)
            {
                WriteError(se);
            }
            catch (Exception e)
            {
                WriteError(e);
            }
        }
        #endregion

        #region Sending Data
        public void SendToAll(string message)
        {
            byte[] _bytes = Encoding.ASCII.GetBytes(message);
            SendToAll(_bytes);
        }

        public void Send(string clientID, string message)
        {
            try
            {
                // Get the data and encode it.
                string _data = String.Format(message);
                byte[] _dataBytes = Encoding.ASCII.GetBytes(_data);

                foreach (SocketPacket _client in m_clients)
                {
                    if (_client != null && _client.Socket.Connected)
                    {
                        if (_client.ID.Equals(clientID))
                        {
                            _client.Socket.BeginSend(_dataBytes, 0, _dataBytes.Length, SocketFlags.None, new AsyncCallback(OnSendClient), _client);
                            return;
                        }
                    }
                }
            }
            catch (SocketException se)
            {
                WriteError(se);
            }
        }

        public void SendToAll(byte[] data)
        {
            try
            {
                byte[] _headerBytes = data;
                foreach (SocketPacket _client in m_clients)
                {
                    if (_client != null && _client.Socket.Connected)
                    {
                        _client.Socket.BeginSend(_headerBytes, 0, _headerBytes.Length, SocketFlags.None, new AsyncCallback(OnSendClient), _client);
                    }
                }
            }
            catch (SocketException se)
            {
                WriteError(se);
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
                WriteError(ode);
            }
            catch (SocketException se)
            {
                WriteError(se);
            }
        }
        #endregion

        #region Game Component Overrides
        public override void Initialize()
        {
            base.Initialize();

            //m_reporter = (IReporterService)this.Game.Services.GetService(typeof(IReporterService));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        #endregion

        #region General Methods and Exception Handling
        protected virtual void WriteError(SocketException exception)
        {
            //if (m_reporter == null)
            //    return;

            //Message msg = new Message(this);
            ////msg.Destination = ((IService)m_reporter).ID;
            //msg.Source = "AsyncServer";
            //msg.Msg = String.Format("SocketErrorCode: {0}, NativeErrorCode: {1}, Message{2}",
            //                        exception.SocketErrorCode.ToString(),
            //                        exception.NativeErrorCode.ToString(),
            //                        exception.Message.ToString());

            //m_reporter.BroadcastError(msg, exception);
        }

        protected virtual void WriteError(ObjectDisposedException exception)
        {
            //if (m_reporter == null)
            //    return;

            //Message msg = new Message(this);
            //msg.Destination = ((IService)m_reporter).ID;
            //msg.Source = "AsyncServer";
            //msg.Msg = String.Format("Object Disposed Exception! ObjectName: {0}, Message: {1}",
            //                        exception.ObjectName, exception.Message);

            //m_reporter.BroadcastError(msg, exception);
        }

        protected virtual void WriteError(ObjectDisposedException exception, string message)
        {
            //if (m_reporter == null)
            //    return;

            //Message msg = new Message(this);
            //msg.Destination = ((IService)m_reporter).ID;
            //msg.Source = "AsyncServer";
            //msg.Msg = String.Format("Object Disposed Exception! ObjectName: {0}, Message: {1}; {2}",
            //                        exception.ObjectName, msg, exception.Message);

            //m_reporter.BroadcastError(msg, exception);
        }

        protected virtual void WriteError(Exception exception)
        {
            //if (m_reporter == null)
            //    return;

            //Message msg = new Message(this);
            //msg.Destination = ((IService)m_reporter).ID;
            //msg.Source = "AsyncServer";
            //msg.Msg = String.Format("Message: {0}", exception.Message);

            //m_reporter.BroadcastError(msg, exception);
        }

        public bool SetPort(string port)
        {
            int _port = 0;
            if (int.TryParse(port, out _port))
            {
                m_port = _port;

                return true;
            }

            return false;
        }
        #endregion

        #region Disposing & Exiting
        protected virtual void OnGameExiting(object sender, EventArgs e)
        {
            Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            Stop();
            base.Dispose(disposing);
        }
        #endregion

        #region Properties
        public string IP
        {
            get
            {
                return m_ip;
            }
        }

        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        public int MaxConnections
        {
            get
            {
                return m_maxConnections;
            }
            set
            {
                m_maxConnections = value;
            }
        }

        public int Port
        {
            get
            {
                return m_port;
            }
            set
            {
                m_port = value;
            }
        }
        #endregion
       
    }
}
