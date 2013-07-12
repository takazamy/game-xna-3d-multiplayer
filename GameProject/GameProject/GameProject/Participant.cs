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
        public int ClientId;
        #endregion

        public Participant(int id)
        {
            hp = 100;
            this.ClientId = id;
        }
    }
}
