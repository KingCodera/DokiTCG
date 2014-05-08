using DokiIRC.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace DokiIRCTest
{
    [TestClass]
    public class IRCTest
    {
        private static IRC client;
        private int counter;

        [ClassInitialize]
        public static void InitTestSuite(TestContext testContext)
        {
            client = new IRC("irc.rizon.net", 6667);
        }

        [ClassCleanup]
        public static void CleanupTestSuite()
        {
        }

        [TestInitialize]
        public void InitTest()
        {
            counter = 0;
        }

        public void CleanupTest()
        {
            counter = 0;
        }

        [TestMethod]
        public void ConnectTest()
        {
            client.Connected += ClientConnected;
            client.Connect();
            client.MessageReceived += OnMessageReceived;
            client.Send("NICK CurePassion\r\n", false);
            client.Send("USER Setsuna 0 * :Setsuna\r\n", false);
            System.Threading.Thread.Sleep(20000);
            client.Send("QUIT :Back to labyrinth!\r\n", true);
            client.Dispose();
        }

        private void OnMessageReceived(IRC i, string msg)
        {
            Console.WriteLine("Received message:");
            string[] parameters = { "\r\n" };
            string[] messages = msg.Split(parameters, StringSplitOptions.RemoveEmptyEntries);
            foreach (string message in messages)
            {
                if (message.StartsWith(":"))
                {
                    Console.WriteLine("{0}", message.Remove(0, 1));
                }
            }

            counter++;
            if (msg.Contains("001"))
            {
                client.Send("JOIN #Severin\r\n", false);
            }
            if (msg.Contains("JOIN"))
            {
                string s = "キュアパッションだよ！";
                client.Send("PRIVMSG #Severin :" + s.Red() + "\r\n", false);
                System.Threading.Thread.Sleep(1000);
                client.Send("PRIVMSG #Severin :" + s.Yellow(COLOURS.BLUE).Italic() + "\r\n", false);
                System.Threading.Thread.Sleep(1000);
                client.Send("PRIVMSG #Severin :" + s.Bold().Underline().Reverse() + "\r\n", false);
            }
        }

        private void ClientConnected(IRC i)
        {
            Console.WriteLine("Client connected.");
        }
    }
}