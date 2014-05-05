using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace DokiTCGTest
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
        public void connectTest()
        {
            client.Connected += ClientConnected;
            client.Connect();
            client.MessageReceived += OnMessageReceived;
            //client.Receive();
            System.Threading.Thread.Sleep(5000);
            client.Dispose();
        }

        private void OnMessageReceived(IRC i, string msg)
        {
            Console.WriteLine("Received message: {0}", msg);
            counter++;
            //client.Receive();
        }

        private void ClientConnected(IRC i)
        {
            Console.WriteLine("Client connected.");
        }
    }
}