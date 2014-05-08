using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokiIRC.Core.Parser
{
    public class Message
    {
        public IPrefix Prefix { get; private set; }

        public ICommand Command { get; private set; }

        public IArgs Args { get; private set; }

        public string Text { get; private set; }

        /// <summary>
        /// Constructs a Message with only a Command, and optional text message.
        /// </summary>
        /// <param name="command"> Command. </param>
        /// <param name="text"> Text message to append to Command. </param>
        public Message(ICommand command, string text = null)
        {
            if (command == null)
            {
                throw new ArgumentNullException("Command can't be null");
            }
            Prefix = null;
            Command = command;
            Args = null;
            Text = text;
        }

        /// <summary>
        /// Contructs a Message with a Command, Arguments, and optional text message.
        /// </summary>
        /// <param name="command"> Command. </param>
        /// <param name="args"> Arguments to Command. </param>
        /// <param name="text"> Text message to append to Command. </param>
        public Message(ICommand command, IArgs args, string text = null)
        {
            if (command == null)
            {
                throw new ArgumentNullException("Command can't be null");
            }
            Prefix = null;
            Command = command;
            Args = args;
            Text = text;
        }

        /// <summary>
        /// Constructs a Message with a Command, Arguments, Prefix, and optional text mesage.
        /// </summary>
        /// <param name="prefix"> Prefix to Command. </param>
        /// <param name="command"> Command. </param>
        /// <param name="args"> Arguments to Command. </param>
        /// <param name="text"> Text message to append to Command. </param>
        public Message(IPrefix prefix, ICommand command, IArgs args, string text = null)
        {
            if (command == null)
            {
                throw new ArgumentNullException("Command can't be null");
            }
            Prefix = prefix;
            Command = command;
            Args = args;
            Text = text;
        }

        /// <summary>
        /// Returns the Message in a format so that it can be sent to an IRC server.
        /// </summary>
        /// <returns> RFC2812 & RFC 1459 compliant message. </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            // Attach prefix if we have one.
            if (Prefix != null)
            {
                sb.AppendFormat("{0} ", Prefix.ToString());
            }

            // Command can't be null;
            sb.AppendFormat("{0}", Command.ToString());

            // Add arguments.
            if (Args != null)
            {
                sb.AppendFormat(" {0}", Args.ToString());
            }

            // Append message.
            if (Text != null)
            {
                sb.AppendFormat(" :{0}", Text);
            }

            // Messages must end with "\r\n".
            sb.Append("\r\n");

            return sb.ToString();
        }
    }
}