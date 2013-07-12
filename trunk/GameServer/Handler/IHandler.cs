using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GameServer.Handler
{
    public abstract class IHandler
    {
        public Client client;
        public IHandler(Client clt)
        {
            this.client = clt;
        }

        public abstract void Handler(JObject data);
    }
}
