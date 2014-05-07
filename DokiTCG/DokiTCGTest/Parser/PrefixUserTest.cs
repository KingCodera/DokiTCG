using DokiTCGIRC.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DokiTCGTest.Parser
{
    /// <summary>
    /// Summary description for PrefixUserTest
    /// </summary>
    [TestClass]
    public class PrefixUserTest
    {
        public PrefixUserTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public const string NICKNAME = "Nickname";
        public const string USERNAME = "Username";
        public const string HOSTNAME = "Hostname";

        public TestContext TestContext { get; set; }

        public PrefixUser Prefix { get; set; }

        #region Additional test attributes

        //
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
            PrefixUser prefix = new PrefixUser(NICKNAME, USERNAME, HOSTNAME);
            string actual = prefix.Mask();
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullNicknameTest()
        {
            PrefixUser prefix = new PrefixUser(null, USERNAME, HOSTNAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullUsernameTest()
        {
            PrefixUser prefix = new PrefixUser(NICKNAME, null, HOSTNAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullHostnameTest()
        {
            PrefixUser prefix = new PrefixUser(NICKNAME, USERNAME, null);
        }

        [TestMethod]
        public void TypeTest()
        {
            PrefixType expected = PrefixType.USER;
            PrefixType actual = Prefix.GetPrefixType();
            Assert.AreEqual(expected, actual);
        }

        // The following tests test illegal and legal characters in nicknames, usernames and hostnames.
        // God I really want parameterised tests for this.

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorNicknameInvalidChar01Test()
        {
            // Nickname must start with letter or special character.
            // (clearly ":" is not a special character)
            PrefixUser prefix = new PrefixUser(":Test", USERNAME, HOSTNAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorNicknameInvalidChar02Test()
        {
            // Nickname can't contain illegal characters suck as Japanese characters!
            PrefixUser prefix = new PrefixUser("御坂", USERNAME, HOSTNAME);
        }

        [TestMethod]
        public void ConstructorHostnameCharSTXTest()
        {
            // Rizon supports control codes in hostnames!
            char STX = '\x02'; // STX is used for bold.
            string s = STX + "04irc.net";
            string expected = NICKNAME + "!" + USERNAME + "@" + s;
            PrefixUser prefix = new PrefixUser(NICKNAME, USERNAME, s);
            string actual = prefix.Mask();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConstructorHostnameCharETXTest()
        {
            char ETX = '\x03'; // ETX is used for colour.
            string s = ETX + "irc.net";
            string expected = NICKNAME + "!" + USERNAME + "@" + s;
            PrefixUser prefix = new PrefixUser(NICKNAME, USERNAME, s);
            string actual = prefix.Mask();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConstructorHostnameCharUSTest()
        {
            char US = '\x1F'; // US is used for underline.
            string s = US + "irc.net";
            string expected = NICKNAME + "!" + USERNAME + "@" + s;
            PrefixUser prefix = new PrefixUser(NICKNAME, USERNAME, s);
            string actual = prefix.Mask();
            Assert.AreEqual(expected, actual);
        }

        // TODO: Add more tests.
    }
}