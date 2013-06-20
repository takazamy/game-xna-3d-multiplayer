using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XnaGameCore.GameLogic.State
{
    public static class States
    {
        public enum ScreenState
        {
            GS_SPLASH_SCREEN = 0,
            GS_MENU = 1,
            GS_LEVEL = 2,
            GS_HELP = 3,
            GS_CREDIT = 4,
            GS_MAIN_GAME = 5,
            GS_ENDGAME = 6,
            GS_EXIT = 7
        };

        public enum ButtonStateEnum
        {
            BS_NORMAL = 0,
            BS_HOLD = 1,
            BS_PRESS = 2,
        }
    }
}
