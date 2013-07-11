using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject
{
    public class Participant
    {
        #region Properties

        public int hp;
        public string nickName;

        #endregion

        public Participant(string nick)
        {
            hp = 100;
            nickName = nick;
        }
    }
}
