using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer
{
    public class Globals
    {
        public static int totalConnection;

        public static string getConfigKey(string name)
        {
            return System.Configuration.ConfigurationSettings.AppSettings[name];
        }
    }
}
