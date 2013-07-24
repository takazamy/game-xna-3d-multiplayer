using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameProject.Network;
using Microsoft.Xna.Framework;
using System.Collections.Concurrent;

namespace GameProject.GameLogic
{
    public class Room
    {
        public ConcurrentDictionary<int, Participant> clientList;
        Client client;
        public int totalPlayer = 0;
        Game game;
        public Room(Game game,Client clt)
        {
            this.game = game;
            client = clt;
            totalPlayer = 0;
            clientList = new ConcurrentDictionary<int, Participant>();
        }

        public void Update(GameTime gameTime)
        {
            lock (clientList)
            {
                try
                {

                    foreach (var item in clientList)
                    {
                        item.Value.Update(gameTime);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                }
            }
           
        }

        public void Add(Participant p)
        {
            lock (clientList)
            {
                clientList.TryAdd(p.ClientId, p);
                totalPlayer++;
            }
        }

        public void Draw(GameTime gameTime)
        {
            lock (clientList)
            {
                foreach (var item in clientList)
                {
                    item.Value.Draw(gameTime);
                }
            }

          

            
        }

        internal void CreateParticipant(int clientId, int pos)
        {
            Participant p = new Participant(clientId, this.game);            
            p.isMe = client.parentParticipant.ClientId == clientId? true:false;
            p.position = pos;
            if (p.isMe == true)
            {
                p.CreateCamera(pos, this.game);
            }
            else
            {
                p.CreateCamera(pos == 1?2:1, this.game);
            }
            this.Add(p);
        }
    }
}
