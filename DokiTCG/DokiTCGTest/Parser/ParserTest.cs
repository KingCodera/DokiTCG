using DokiTCGIRC.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DokiTCGTest.Parser
{
    /// <summary>
    /// These tests will absolutely destroy an incorrectly programmed parser!
    /// </summary>
    [TestClass]
    public class ParserTest
    {
        public const PrefixServer PREFIX_SERVER = new PrefixServer("irc.test.com");
        public const PrefixUser PREFIX_USER = new PrefixUser("Nickname", "Username", "host.com");

        public ParserTest()
        {
            //
            // TODO: Add constructor logic here
            // DONE: A = B & B = C Therefor: A = C
            //
        }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            // Need more lemon pledge.
        }

        #endregion Additional test attributes

        [TestMethod]
        public void ParseRPL_WELCOME()
        {
            ICommand Command = null; // TODO: Make this not null by implementing Command!
            string text = "Welcome";
            Message expected = new Message(PREFIX_SERVER, Command, null, text);
            Message actual = MessageParser.Parse(":irc.test.com 001 Nickname :Welcome");
            Assert.AreEqual(expected, actual);
        }
    }
}