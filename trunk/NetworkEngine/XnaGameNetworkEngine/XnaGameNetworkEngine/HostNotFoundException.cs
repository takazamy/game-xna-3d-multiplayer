using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XnaGameNetworkEngine
{
    public class HostNotFoundException : Exception
    {
        public HostNotFoundException(string host)
            : base(String.Format("Host ({0}) could not be found.", host))
        {
        }
    }
}
