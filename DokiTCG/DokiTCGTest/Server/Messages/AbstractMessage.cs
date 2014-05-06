using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokiTCGTest.Server.Messages
{
    public abstract class AbstractMessage : IMessage
    {
        AbstractMessage(string command, string prefix)
        {
            this.command = command;
            this.prefix = prefix;
        }
    }
}
