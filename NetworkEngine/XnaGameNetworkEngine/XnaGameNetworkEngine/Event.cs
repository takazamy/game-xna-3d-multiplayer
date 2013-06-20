using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XnaGameNetworkEngine
{
    #region Server Only
    public delegate void ClientConnectedHandler(SocketPacket packet);
    public delegate void ClientDisconnectedHandler(SocketPacket packet);
    public delegate void ClientAuthRequestHandler(SocketPacket packet);

    public delegate void ServerStartedHandler();
    public delegate void ServerStoppedHandler();
    #endregion

    public delegate void DataReceivedHandler(string data, SocketPacket packet);

    #region Client Only
    public delegate void ConnectedHandler(SocketPacket server);
    public delegate void DisconnectedHandler();
    #endregion
}
