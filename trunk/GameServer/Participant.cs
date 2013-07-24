using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace GameServer
{
    public class Participant
    {
        #region Properties

        public int hp;        
        public int ClientId;
        public int position;
        public Vector3 rotateAngle;
        public Vector3 phapTuyen;
        #endregion

        public Participant(int clientId)
        {
            hp = 100;            
            ClientId = clientId;
            rotateAngle = new Vector3();
            phapTuyen = new Vector3();
        }

        public void reset()
        {
            hp = 100;
        }

    }
}
