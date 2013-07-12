using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GameProject.GameLogic;
namespace GameProject.Network
{
    public abstract class IHandler
    {
        public Client clt;
        public IHandler(Client clt)
        {
            this.clt = clt;
        }

        public abstract void Handler(JObject data);
        
        
    }
}
