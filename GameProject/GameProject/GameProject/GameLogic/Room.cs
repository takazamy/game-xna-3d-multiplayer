using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameProject.Network;
using Microsoft.Xna.Framework;

namespace GameProject.GameLogic
{
    public class Room
    {
        public Dictionary<int, Participant> clientList;
        Client clt;
        public int totalPlayer = 0;
        public Room()
        {
            totalPlayer = 0;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var item in clientList)
            {
                item.Value.Update(gameTime);
            }
        }

        public void Add(Participant p)
        {
            clientList.Add(p.ClientId, p);
            totalPlayer++;
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var item in clientList)
            {
                item.Value.Draw(gameTime);
            }

            
        }
    }
}
