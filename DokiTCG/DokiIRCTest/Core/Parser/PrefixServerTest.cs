using DokiIRC.Core.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DokiIRCTest.Parser
{
    [TestClass]
    public class PrefixServerTest
    {
        public const string HOSTNAME = "Hostname";

        public PrefixServer Prefix { get; set; }

        #region Additional test attributes

        [TestInitialize()]
        public void MyTestInitialize()
        {
            Prefix = new PrefixServer(HOSTNAME);
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
            string expected = HOSTNAME;
            PrefixServer prefix = new PrefixServer(HOSTNAME);
            string actual = prefix.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringTest()
        {
            string expected = HOSTNAME;
            string actual = Prefix.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TypeTest()
        {
            PrefixType expected = PrefixType.SERVER;
            PrefixType actual = Prefix.GetPrefixType();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullHostnameTest()
        {
            Prefix = new PrefixServer(null);
        }

        // Illegal characters tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HostnameInvalidChar01Test()
        {
            Prefix = new PrefixServer("~servername");
        }
    }
}