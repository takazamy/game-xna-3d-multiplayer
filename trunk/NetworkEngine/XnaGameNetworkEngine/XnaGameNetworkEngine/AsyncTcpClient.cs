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
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace XnaGameNetworkEngine
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class AsyncTcpClient : Microsoft.Xna.Framework.GameComponent
    {

        #region Members
        // Our connection to the server
        protected SocketPacket m_socket;

        // Callback stuff
        private IAsyncResult m_result;
        private AsyncCallback m_clientCallback;

        // The port we are working with
        private int m_port;

        // The IP Address we want to connect to.
        private string m_ipAddr;

        // The Reporting service.
        //protected IReporterService m_reporter;

        // ManualResetEvent instances signal completion.
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        public ManualResetEvent sendDone = new ManualResetEvent(false);
        #endregion

        #region Events
        public event DataReceivedHandler DataReceived;
        public event ConnectedHandler Connected;
        public event DisconnectedHandler Disconnected;
        #endregion

        #region Constructor
        public AsyncTcpClient(Game game)
            : base(game)
        {
            this.Connected += new ConnectedHandler(OnConnected);
            this.Disconnected += new DisconnectedHandler(OnDisconnected);

            this.DataReceived += new DataReceivedHandler(OnDataReceived);

            game.Exiting += new EventHandler<EventArgs>(OnGameExiting);
        }

        ~AsyncTcpClient()
        {
            Dispose(true);
        }
        #endregion

        #region Virtual Event Handlers
        protected virtual void OnDisconnected()
        {
        }

        protected virtual void OnConnected(SocketPacket server)
        {
        }

        protected virtual void OnDataReceived(string data, SocketPacket packet)
        {
        }
        #endregion

        #region Connecting & Disconnecting
        public void Connect(string ip, int port)
        {
            // Set the IP Address and Port
            m_ipAddr = ip;
            m_port = port;

            // Attempt to connect!
            Connect();
        }

        public void Connect()
        {
            try
            {
                // Make sure the IP Address and Port are valid
                if (String.IsNullOrEmpty(m_ipAddr) || m_port == 0)
                    return;

                // Make sure the IP Address exists
                IPHostEntry entry = Dns.GetHostEntry(m_ipAddr);

                // If it doesn't, throw an exception.
                if (entry == null)
                    throw new HostNotFoundException(m_ipAddr);

                // Create the IP Endpoint object
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(m_ipAddr), m_port);

                // Create our connection object
                m_socket = new SocketPacket("Server");

                // Create the socket
                m_socket.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Attempt to connect
                m_socket.Socket.BeginConnect(ipEnd, new AsyncCallback(OnEndConnect), m_socket);
                
                // Wait till the connection is made
                connectDone.WaitOne();
            }
            catch (HostNotFoundException hnf)
            {
                WriteError(hnf);
            }
            catch (SocketException se)
            {
                //string _str = "Connection failed, is the server running?\n" + se.Message;

                WriteError(se);
            }
            catch (Exception e)
            {
                WriteError(e);
            }
        }

        public void Disconnect()
        {
            // Close, Disconnect and Shutdown the connection.
            if (m_socket != null)
            {
                if (m_socket.Socket.Connected)
                {
                    m_socket.Socket.Disconnect(false);
                    m_socket.Socket.Shutdown(SocketShutdown.Both);
                    m_socket.Socket.Close();
                }
                // Clear the buffer.
                m_socket.ResetBuffer();

                // Invoke the Disconnected event.
                if (Disconnected != null)
                    Disconnected.Invoke();
            }
        }
        #endregion

        #region Callbacks and Socket Handling
        protected virtual void WaitForData()
        {
            try
            {
                // Create our callback.
                if (m_clientCallback == null)
                    m_clientCallback = new AsyncCallback(OnServerDataReceived);

                // Begin receiving data
                m_result = m_socket.Socket.BeginReceive(m_socket.DataBuffer, 0, m_socket.BufferLength, SocketFlags.None, m_clientCallback, m_socket);
            }
            catch (SocketException se)
            {
                WriteError(se);
            }
        }

        protected virtual void OnEndConnect(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                SocketPacket client = (SocketPacket)ar.AsyncState;

                // Complete the connection.
                client.Socket.EndConnect(ar);

                // Signal that the connection has been made.
                connectDone.Set();

                // Wait for data to come in asynchronously
                WaitForData();

                // Invoke the Connected event.
                if (Connected != null)
                    Connected.Invoke(m_socket);
            }
            catch (Exception e)
            {
                WriteError(e);
            }
        }

        private void OnServerDataReceived(IAsyncResult ar)
        {
            try
            {
                SocketPacket _client = (SocketPacket)ar.AsyncState;
                int iRx = 0;
                iRx = _client.Socket.EndReceive(ar);

                if (iRx <= 0)   // If nothing was read return
                    return;

                string _strData = Encoding.ASCII.GetString(_client.DataBuffer, 0, iRx);
                if (DataReceived != null)
                    DataReceived.Invoke(_strData, _client);

                _client.ResetBuffer();

                WaitForData();

            }
            catch (ObjectDisposedException ode)
            {
                WriteError(ode, "OnClientDataReceived: Socket has been closed.");
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

        #region Sending

        /// <summary>
        /// Send data to a specific client.
        /// </summary>
        /// <param name="clientID">The client's ID you want to send to.</param>
        /// <param name="message">The data to be sent.</param>
        public void Send(string clientID, string message)
        {
            try
            {
                // Get the data and encode it.
                string _data = String.Format("<TO>{0}</TO>" + message, clientID);
                byte[] _dataBytes = Encoding.ASCII.GetBytes(_data);

                // Begin sending the data
                m_socket.Socket.BeginSend(_dataBytes, 0, _dataBytes.Length, SocketFlags.None, new AsyncCallback(OnSendData), m_socket);

            }
            catch (SocketException se)
            {
                WriteError(se);
            }
        }

        public void OnSendData(IAsyncResult ar)
        {
            try
            {
                // Get the connection
                SocketPacket _client = (SocketPacket)ar.AsyncState;

                // Complete the send.
                int _sent = _client.Socket.EndSend(ar);

                // Signal that all bytes have been sent.
                sendDone.Set();
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

        #region Error Writing
        /// <summary>
        /// Report a SocketException
        /// </summary>
        /// <param name="exception">The SocketException to report.</param>
        protected virtual void WriteError(SocketException exception)
        {
            //if (m_reporter == null)
            //    return;

            //Message msg = new Message(this);
            //msg.Destination = ((IService)m_reporter).ID;
            //msg.Source = "AsyncServer";
            //msg.Msg = String.Format("SocketErrorCode: {0}, NativeErrorCode: {1}, Message{2}",
            //                        exception.SocketErrorCode.ToString(),
            //                        exception.NativeErrorCode.ToString(),
            //                        exception.Message.ToString());

            //m_reporter.BroadcastError(msg, exception);
        }

        /// <summary>
        /// Report a HostNotFoundException
        /// </summary>
        /// <param name="exception">The HostNotFoundException to report.</param>
        protected virtual void WriteError(HostNotFoundException exception)
        {
            //if (m_reporter == null)
            //    return;

            //Message msg = new Message(this);
            //msg.Destination = ((IService)m_reporter).ID;
            //msg.Source = "AsyncServer";
            //msg.Msg = exception.Message;

            //m_reporter.BroadcastError(msg, exception);
        }

        /// <summary>
        /// Report an Exception.
        /// </summary>
        /// <param name="exception">The Exception to report.</param>
        protected virtual void WriteError(Exception exception)
        {
            //if (m_reporter == null)
            //    return;

            //Message msg = new Message(this);
            //msg.Destination = ((IService)m_reporter).ID;
            //msg.Source = "AsyncServer";
            //msg.Msg = exception.Message;

            //m_reporter.BroadcastError(msg, exception);
        }

        /// <summary>
        /// Report an ObjectDisposedException.
        /// </summary>
        /// <param name="exception">The ObjectDisposedException to report.</param>
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

        /// <summary>
        /// Report an ObjectDisposedException with a message.
        /// </summary>
        /// <param name="exception">The ObjectDisposedException to report.</param>
        /// <param name="message">The message to send.</param>
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
        #endregion

        #region Disposing & Exiting
        protected virtual void OnGameExiting(object sender, EventArgs e)
        {
            Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Disconnect();
        }
        #endregion

    }
}
