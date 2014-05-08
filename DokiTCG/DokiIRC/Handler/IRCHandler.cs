using DokiIRC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokiIRC.Handler
{
    public class IRCHandler : IDisposable
    {
        private IRC connection;

        public IRCHandler(IRC connection)
        {
            this.connection = connection;
            this.connection.Connected += OnConnected;
            this.connection.MessageReceived += OnMessageReceived;
            this.connection.MessageSent += OnMessageSent;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void OnConnected(IRC i)
        {
            throw new NotImplementedException();
        }

        private void OnMessageReceived(IRC i, string msg)
        {
            throw new NotImplementedException();
        }

        private void OnMessageSent(IRC i, bool close)
        {
            throw new NotImplementedException();
        }
    }
}