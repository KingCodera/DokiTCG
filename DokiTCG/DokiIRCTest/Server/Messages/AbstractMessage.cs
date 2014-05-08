using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokiIRCTest.Server.Messages
{
    public abstract class AbstractMessage : IMessage
    {
        protected string message { get; set; }

        protected string command { get; set; }

        public AbstractMessage()
        {
        }

        public AbstractMessage(string message)
        {
            this.message = message;
        }
    }
}