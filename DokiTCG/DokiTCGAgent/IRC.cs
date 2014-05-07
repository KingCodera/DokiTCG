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

        // Connects to a server using a nickname and username.
        public void ConnectDirect(string nick, string username)
        {
        }

        /// <summary>
        /// Connects to a server using a password.
        /// </summary>
        /// <param name="password"> Password of the server. </param>
        public void ConnectPassword(string nick, string username, string password)
        {
        }

        /// <summary>
        /// Checks if the server is connected.
        /// </summary>
        /// <returns> True of False. </returns>
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
            // If you need to read this comment to understand what is going on, you better
            // pick another profession.
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

        /// <summary>
        /// Receives data from the socket.
        /// This function is contineously called by ReceiveCallback.
        /// </summary>
        private void Receive()
        {
            receiveDone.Reset();
            StateObject state = new StateObject();
            state.listener = ircSocket;
            state.listener.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
        }

        /// <summary>
        /// Asynchronous function handling the receiving of a message.
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            // Convert AsyncState to StateObject.
            StateObject state = ar.AsyncState as StateObject;
            // Check number of bytes received in buffer.
            int receive = state.listener.EndReceive(ar);
            // If we received more than 0 bytes, append string to string builder.
            if (receive > 0)
            {
                state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, receive));
            }
            // If the number of bytes received is equal to the buffer size, do another call to empty the buffer.
            // Else notify the client that we received a message and start listening for the next.
            if (receive == StateObject.BUFFER_SIZE)
            {
                state.listener.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
            }
            else
            {
                MessageReceivedHandler handler = MessageReceived;
                if (handler != null)
                {
                    handler(this, state.sb.ToString());
                }
                // Empty string builder.
                state.sb = new StringBuilder();
                receiveDone.Set();
                Receive();
            }
        }

        #endregion Receive Data

        #region Send Data

        /// <summary>
        /// Sends data to the server.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="close"></param>
        public void Send(string msg, bool close)
        {
            if (!IsConnected())
            {
                // Can't send messages if we're not connected.
                throw new Exception("Destination socket is not connected.");
            }
            // Encode message in UTF8, since it's past 1960.
            byte[] response = Encoding.UTF8.GetBytes(msg);
            // Should we close the server after sending this message? - Only used for QUIT message.
            this.close = close;
            ircSocket.BeginSend(response, 0, response.Length, SocketFlags.None, new AsyncCallback(SendCallback), ircSocket);
        }

        /// <summary>
        /// Asynchronous function handling the sending of the message.
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            // Attempt to retrieve the socket from the IAsyncResult.
            try
            {
                Socket receiver = ar.AsyncState as Socket;
                // Wait for Send to complete.
                receiver.EndSend(ar);
            }
            catch (SocketException se)
            {
                // Socket has an error.
            }
            catch (ObjectDisposedException oe)
            {
                // Son, are you trying to send messages over a socket that no longer exists?
            }
            // Notify that message is sent via event.
            MessageSentHandler handler = MessageSent;
            // Silly user, you can't raise an event when no one is listening.
            if (handler != null)
            {
                handler(this, this.close);
            }
            // Message has been sent!
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

        /// <summary>
        /// Asynchronous function handling the connection to the server.
        /// </summary>
        /// <param name="ar"></param>
        private void OnConnectCallback(IAsyncResult ar)
        {
            // Retrieve socket from AsyncState.
            Socket server = ar.AsyncState as Socket;
            try
            {
                // Wait for socket to be connected.
                server.EndConnect(ar);
                // Notify we're connected.
                connected.Set();
            }
            catch (SocketException e)
            {
                // Something went wrong.
                // TODO: Figure out what can go wrong with a Socket.EndConnect request.
            }
        }
    } /* IRC */

    /// <summary>
    /// StateObject for messages to and from the server.
    /// </summary>
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