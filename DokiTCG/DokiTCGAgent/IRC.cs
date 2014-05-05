using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
    public class IRC : IDisposable
    {
        private const int TIMEOUT = 1000;
        private Socket ircSocket;
        private Boolean close = false;
        private IPHostEntry ipHostInfo;
        private IPAddress[] ipAddress;
        private int port;

        #region Event Handlers

        public delegate void ConnectedHandler(IRC i);

        public event ConnectedHandler Connected;

        public delegate void MessageReceivedHandler(IRC i, string msg);

        public event MessageReceivedHandler MessageReceived;

        public delegate void MessageSentHandler(IRC i, bool close);

        public event MessageSentHandler MessageSent;

        #endregion Event Handlers

        // ASyncCallback reset events.
        private ManualResetEvent dnsResolveDone = new ManualResetEvent(false);

        private ManualResetEvent connected = new ManualResetEvent(false);
        private ManualResetEvent sentDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        /// <summary>
        /// Constructor for the IRC connection.
        /// </summary>
        /// <param name="host"> URI of the server to connect to. </param>
        /// <param name="port"> Port of the server to connect to. (0 to 65535) </param>
        public IRC(string host, int port)
        {
            // Check if the port number is valid.
            if (!CheckValidPort(port))
            {
                throw new InvalidPortException("Port is not within valid range (0-65535).");
            }

            this.port = port;

            // Resolve hostname.
            Dns.BeginGetHostEntry(host, new AsyncCallback(OnDNSCallback), "Looking up DNS Record.");
            // Wait for DNS Server to return result.
            if (!dnsResolveDone.WaitOne(TIMEOUT))
            {
                // Unable to resolve host name.
                throw new InvalidHostException("DNS Server was unable to resolve host name.");
            }

            // Check if DNS Server can resolve hostname.
            if (ipHostInfo.AddressList.Length == 0)
            {
                throw new InvalidHostException("DNS Server did not return any IP addresses.");
            }

            ipAddress = ipHostInfo.AddressList;
        }

        private void Close()
        {
            try
            {
                if (IsConnected())
                {
                    ircSocket.Shutdown(SocketShutdown.Both);
                    ircSocket.Close();
                }
            }
            catch (SocketException e)
            {
                // TODO: Write Exception Handling
            }
        }

        public void Dispose()
        {
            connected.Close();
            sentDone.Close();
            receiveDone.Close();
            Close();
        }

        public void Connect()
        {
            int index = 0;
            Boolean con = false;
            while (!con && index < ipAddress.Length)
            {
                IPEndPoint remoteEP = new IPEndPoint(ipAddress[index], port);
                try
                {
                    // TODO: Hardcoded network properties, IRC servers support IPv6 nowadays.
                    ircSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ircSocket.BeginConnect(remoteEP, new AsyncCallback(OnConnectCallback), ircSocket);
                    if (connected.WaitOne(TIMEOUT))
                    {
                        ConnectedHandler handler = Connected;
                        if (handler != null)
                        {
                            Connected(this);
                        }
                        con = true;
                    }
                    else
                    {
                        index++;
                    }
                }
                catch (SocketException e)
                {
                    // Unable to connect.
                    Console.WriteLine("Could not connect to address: {0}:{1}", ipAddress[index], port);
                }
            }
            if (con)
            {
                Receive();
            }
            else
            {
                Console.WriteLine("Unable to connect.");
                Dispose();
            }
        }

        public void ConnectDirect(string nick, string password)
        {
        }

        public void ConnectBNC(string password)
        {
        }

        public Boolean IsConnected()
        {
            return !(ircSocket.Poll(1000, SelectMode.SelectRead) && ircSocket.Available == 0);
        }

        /// <summary>
        /// Checks the port number is valid.
        /// </summary>
        /// <param name="port"> Port Number to be checked. </param>
        /// <returns> True of False. </returns>
        private Boolean CheckValidPort(int port)
        {
            if (port < 0 || port > 65535)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region Receive Data

        public void Receive()
        {
            StateObject state = new StateObject();
            state.listener = ircSocket;
            state.listener.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = ar.AsyncState as StateObject;

            int receive = state.listener.EndReceive(ar);
            if (receive > 0)
                state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, receive));
            if (receive == StateObject.BUFFER_SIZE)
                state.listener.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
            else
            {
                MessageReceivedHandler handler = MessageReceived;
                if (handler != null)
                {
                    handler(this, state.sb.ToString());
                }
                state.sb = new StringBuilder();
                receiveDone.Set();
                Receive();
            }
        }

        #endregion Receive Data

        #region Send Data

        public void Send(string msg, bool close)
        {
            if (!IsConnected())
            {
                throw new Exception("Destination socket is not connected.");
            }
            byte[] response = Encoding.UTF8.GetBytes(msg);
            this.close = close;
            ircSocket.BeginSend(response, 0, response.Length, SocketFlags.None, new AsyncCallback(SendCallback), ircSocket);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket receiver = ar.AsyncState as Socket;
                receiver.EndSend(ar);
            }
            catch (SocketException se)
            {
            }
            catch (ObjectDisposedException oe)
            {
            }
            MessageSentHandler handler = MessageSent;
            if (handler != null)
            {
                handler(this, this.close);
            }
            sentDone.Set();
        }

        #endregion Send Data

        /// <summary>
        /// DNS Resolve ASyncCallback.
        /// </summary>
        /// <param name="ar"> Result. </param>
        private void OnDNSCallback(IAsyncResult ar)
        {
            try
            {
                // Get the IPHostEntry data.
                ipHostInfo = Dns.EndGetHostEntry(ar);
                // Notify ASyncCallback is done.
                dnsResolveDone.Set();
            }
            catch (Exception e)
            {
                // Something went wrong.
                // TODO: Figure out what can go wrong with a Dns.EndGetHostEntry request.
            }
        }

        private void OnConnectCallback(IAsyncResult ar)
        {
            Socket server = ar.AsyncState as Socket;
            try
            {
                server.EndConnect(ar);
                connected.Set();
            }
            catch (SocketException e)
            {
                // Something went wrong.
                // TODO: Figure out what can go wrong with a Socket.EndConnect request.
            }
        }
    } /* IRC */

    public class StateObject
    {
        public Socket listener = null;
        public const int BUFFER_SIZE = 20480;
        public byte[] buffer = new byte[BUFFER_SIZE];

        public StringBuilder sb = new StringBuilder();

        public int GetBufferSize()
        {
            return BUFFER_SIZE;
        }
    } /* StateObject */
} /* System.Net */