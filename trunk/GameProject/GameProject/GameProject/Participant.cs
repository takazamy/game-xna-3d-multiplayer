using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Participant
    {
        #region Properties
        Turret turret;
        public int hp;
        public string nickName;
        public int ClientId;
        #endregion

        public Participant(int id)
        {
            hp = 100;
            this.ClientId = id;
        }

        public void Init()
        {

        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {

        }
    }
}
