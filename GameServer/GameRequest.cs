using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GameServer
{
    public static class GameRequest
    {
        public static void sendConnected(Client clt, int id)
        {
            JObject mesg = new JObject();
            mesg[GameCommand.COMMAND] = GameCommand.CONNECT;
            mesg[GameKeys.ID] = clt.parentParticipant.ClientId;
            clt.send(mesg.ToString());
        }

        public static void sendCreateRoom(Client clt)
        {
            JObject mesg = new JObject();
            mesg[GameCommand.COMMAND] = GameCommand.CREATE_GAME;
            mesg[GameKeys.ROOMID] = clt.refRoom.roomId;
            clt.send(mesg.ToString());
        }

        public static void sendJoinRoom(Client clt, bool success)
        {
            JObject mesg = new JObject();
            mesg[GameCommand.COMMAND] = GameCommand.JOIN_ROOM;
            mesg[GameKeys.SUCCESS] = success;
            clt.send(mesg.ToString());
        }
    }
}
