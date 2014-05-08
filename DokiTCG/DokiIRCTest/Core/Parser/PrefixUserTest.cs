using DokiIRC.Core.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DokiIRCTest.Core.Parser
{
    /// <summary>
    /// Summary description for PrefixUserTest
    /// </summary>
    [TestClass]
    public class PrefixUserTest
    {
        public const string NICKNAME = "Nickname";
        public const string USERNAME = "Username";
        public const string HOSTNAME = "Hostname";

        public PrefixUser Prefix { get; set; }

        #region Additional test attributes

        [TestInitialize()]
        public void MyTestInitialize()
        {
            Prefix = new PrefixUser(NICKNAME, USERNAME, HOSTNAME);
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            Prefix = null;
        }

        #endregion Additional test attributes

        [TestMethod]
        public void ConstructorTest()
        {
            string expected = "Nickname!Username@Hostname";
            string actual = Prefix.Mask();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringTest()
        {
            string expected = NICKNAME;
            string actual = Prefix.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TypeTest()
        {
            PrefixType expected = PrefixType.USER;
            PrefixType actual = Prefix.GetPrefixType();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullNicknameTest()
        {
            Prefix = new PrefixUser(null, USERNAME, HOSTNAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullUsernameTest()
        {
            Prefix = new PrefixUser(NICKNAME, null, HOSTNAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullHostnameTest()
        {
            Prefix = new PrefixUser(NICKNAME, USERNAME, null);
        }

        // The following tests test illegal and legal characters in nicknames, usernames and hostnames.
        // God I really want parameterised tests for this.

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NicknameInvalidChar01Test()
        {
            // Nickname must start with letter or special character.
            // (clearly ":" is not a special character)
            Prefix = new PrefixUser(":Test", USERNAME, HOSTNAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NicknameInvalidChar02Test()
        {
            // Nickname can't contain illegal characters suck as Japanese characters!
            Prefix = new PrefixUser("御坂", USERNAME, HOSTNAME);
        }

        [TestMethod]
        public void HostnameCharSTXTest()
        {
            // Rizon supports control codes in hostnames!
            char STX = '\x02'; // STX is used for bold.
            string s = STX + "04irc.net";
            string expected = NICKNAME + "!" + USERNAME + "@" + s;
            Prefix = new PrefixUser(NICKNAME, USERNAME, s);
            string actual = Prefix.Mask();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HostnameCharETXTest()
        {
            char ETX = '\x03'; // ETX is used for colour.
            string s = ETX + "irc.net";
            string expected = NICKNAME + "!" + USERNAME + "@" + s;
            Prefix = new PrefixUser(NICKNAME, USERNAME, s);
            string actual = Prefix.Mask();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HostnameCharUSTest()
        {
            char US = '\x1F'; // US is used for underline.
            string s = US + "irc.net";
            string expected = NICKNAME + "!" + USERNAME + "@" + s;
            Prefix = new PrefixUser(NICKNAME, USERNAME, s);
            string actual = Prefix.Mask();
            Assert.AreEqual(expected, actual);
        }

        // TODO: Add more tests.
    }
}