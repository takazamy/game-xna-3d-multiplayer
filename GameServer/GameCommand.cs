﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer
{
    public static class GameCommand
    {
        public const string COMMAND = "cmd";
        //game logic keys
        public const string CONNECT = "connect";
        public const string CREATE_GAME = "create";
        public const string GET_LIST_ROOM = "getlistroom";
        public const string JOIN_ROOM = "join";
        public const string MOVE = "move";
        public const string SHOOT = "shoot";
    }
}
