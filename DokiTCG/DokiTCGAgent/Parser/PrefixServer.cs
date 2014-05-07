using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokiTCGIRC.Parser
{
    public class PrefixServer : AbstractPrefix
    {
        public string ServerName { get; private set; }

        public PrefixServer(string servername)
        {
            ServerName = servername;
        }

        public override PrefixType GetPrefixType()
        {
            return PrefixType.SERVER;
        }

        public override string ToString()
        {
            return ServerName;
        }
    }
}