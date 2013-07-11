using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer
{
    public class Participant
    {
        #region Properties

        public int hp;        
        public int ClientId;
        #endregion

        public Participant(int clientId)
        {
            hp = 100;            
            ClientId = clientId;
        }

        public void reset()
        {
            hp = 100;
        }
    }
}
