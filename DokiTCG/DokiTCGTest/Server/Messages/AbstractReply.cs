using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokiTCGTest.Server.Messages
{
    internal abstract class AbstractReply
    {
        protected IMessage message;

        public AbstractReply()
        {
        }

        public AbstractReply(String message, String type)
        {
        }
    }
}