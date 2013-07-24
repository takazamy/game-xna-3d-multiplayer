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
        Client client;
        public int totalPlayer = 0;
        Game game;
        public Room(Game game,Client clt)
        {
            this.game = game;
            client = clt;
            totalPlayer = 0;
            clientList = new Dictionary<int, Participant>();
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

        internal void CreateParticipant(int clientId)
        {
            Participant p = new Participant(clientId, this.game);            
            p.isMe = client.parentParticipant.ClientId == clientId? true:false;           
            p.CreateCamera(totalPlayer == 0? 1:2, this.game);
            this.Add(p);
        }
    }
}
