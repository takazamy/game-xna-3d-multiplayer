using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject.GameLogic
{
    public static class GameKeys
    {
        //game properties keys
        public const string ID = "id";
        public const string ROOMID = "roomid";
        public const string NUMBER_USERS = "numberusers";
        public const string MAX = "max";
        public const string ROOM_LIST = "roomlist";
        public const string SUCCESS = "success";
        public const string POSITION = "pos";
        public const string INFO = "info";
        public const string LEFTRIGHTMOVE = "lrm";
        public const string UPDOWNMOVE = "udm";

        public enum TURRET_STATE_UD
        {
            STAYUD = 0,
            UP = 1,
            DOWN = -1
        }
        public enum TURRET_STATE_LR
        {
            STAYLR = 0,
            LEFT = 1,
            RIGHT = -1
        }
        //public static const string 
    }
}
