using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokiTCGTest.Server.Messages
{
    internal class RPL_WELCOME : AbstractMessage
    {
        public RPL_WELCOME()
        {
            this.command = "001";
            this.message = "Welcome to the IRC Test Server";
        }
    }
}