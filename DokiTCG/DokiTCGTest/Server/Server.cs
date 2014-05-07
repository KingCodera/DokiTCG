using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DokiTCGTest.Server
{
    internal class Server
    {
        private String name;
        private Boolean shutdown = false;

        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public Server(String name)
        {
            this.name = name;
        }

        private void Listen()
        {
            byte[] bytes = new Byte[1024];

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 6667);

            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (!shutdown)
                {
                    allDone.Reset();
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                // Herpaderp error.
                Console.WriteLine("Exception occured: {0}", e.ToString());
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();
            Socket listener = ar.AsyncState as Socket;
            Socket handler = listener.EndAccept(ar);
            StateObject state = new StateObject();
            state.listener = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(ReceiveCallback), state);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            String content = String.Empty;
            StateObject state = ar.AsyncState as StateObject;
            Socket handler = state.listener;
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));
                content = state.sb.ToString();
                ContentHandler(content);
            }
            else
            {
                handler.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(ReceiveCallback), state);
            }
        }

        public void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        /// <summary>
        /// Processes messages received from clients according to RFC 2812.
        /// Link to RFC: http://www.faqs.org/rfcs/rfc2812.html
        /// </summary>
        /// <param name="content"> Message from user. </param>
        private void ContentHandler(String content)
        {
            String[] data = content.Split(' ');
            switch (data[0].ToUpper())
            {
                // Connection registration.
                case "NICK": break;
                case "USER": break;
                case "PASS": break;
                case "OPER": break; // OPER command.
                case "MODE": break; // MODE <nickname> *( ( "+" / "-" ) *( "i" / "w" / "o" / "O" / "r" ) )
                case "SERVICE": break; // SERVICE command.
                case "QUIT": break;
                case "SQUIT": break; // OPER command.

                // Channel operations.
                case "JOIN": break;
                case "PART": break;
                // MODE overloaded call // MODE <channel> *( ( "-" / "+" ) *<modes> *<modeparams> )
                case "TOPIC": break;
                case "NAMES": break;
                case "LIST": break;
                case "INVITE": break;
                case "KICK": break;

                // Sending messages.
                case "PRIVMSG": break;
                case "NOTICE": break;

                // Server queries and commands.
                case "MOTD": break;
                case "LUSERS": break;
                case "VERSION": break;
                case "STATS": break;
                case "LINKS": break;
                case "TIME": break;
                case "CONNECT": break; // OPER command.
                case "TRACE": break;
                case "ADMIN": break;
                case "INFO": break;

                // Service Query and Commands.
                case "SERVLIST": break;
                case "SQUERY": break; // SERVICE command.

                // User based queries.
                case "WHO": break;
                case "WHOIS": break;
                case "WHOWAS": break;

                // Miscellaneous messages.
                case "KILL": break; // OPER command.
                case "PING": break;
                case "PONG": break;
                case "ERROR": break;

                // Optional features.
                case "AWAY": break;
                case "REHASH": break; // OPER command. NOT SUPPORTED YET IN TEST!
                case "DIE": break; // OPER command. NOT SUPPORTED YET IN TEST!
                case "RESTART": break; // OPER command. NOT SUPPORTED YET IN TEST!
                case "SUMMON": break; // OPER command. NOT SUPPORTED YET IN TEST!
                case "USERS": break;
                case "WALLOPS": break; // OPER command. NOT SUPPORTED YET IN TEST!
                case "USERHOST": break;
                case "ISON": break;

                // Unrecognised command.
                default: break;
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = ar.AsyncState as Socket;
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception e)
            {
                // HerpaDerp error.
                Console.WriteLine("Exception occured: {0}", e.ToString());
            }
        }
    }

    public class StateObject
    {
        public Socket listener = null;
        public const int BUFFER_SIZE = 1024;
        public byte[] buffer = new byte[BUFFER_SIZE];
        public StringBuilder sb = new StringBuilder();
    }
}