using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GameProject.GameLogic;
using Microsoft.Xna.Framework;

namespace GameProject.Network
{
    public static class RequestHandler
    {
        public static void SendCreateGame(Client clt)
        {
            JObject mesg = new JObject();
            mesg[GameCommand.COMMAND] = GameCommand.CREATE_GAME;
            clt.send(mesg.ToString());
        }

        public static void SendGetListRoom(Client clt)
        {
            JObject mesg = new JObject();
            mesg[GameCommand.COMMAND] = GameCommand.GET_LIST_ROOM;
            
            clt.send(mesg.ToString());
        }

        public static void SendJoinRoom(Client clt, int roomId)
        {
            JObject mesg = new JObject();
            mesg[GameCommand.COMMAND] = GameCommand.JOIN_ROOM;
            mesg[GameKeys.ROOMID] = roomId;
            clt.send(mesg.ToString());
        }

        public static void SendPlayerMove(Client clt, int stateLR, int stateUD)
        {
            JObject mesg = new JObject();
            mesg[GameCommand.COMMAND] = GameCommand.MOVE;
            mesg[GameKeys.LEFTRIGHTMOVE] = (int)stateLR;
            mesg[GameKeys.UPDOWNMOVE] = (int)stateUD;
            clt.send(mesg.ToString());

        }
    }
}
