using ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DokiIRCTest.String
{
    [TestClass]
    public class StringTest
    {
        [TestMethod]
        public void StringRedTest()
        {
            string expected = '\x03' + "04Test";
            string actual = "Test".Red();
            Assert.AreEqual(expected, actual);
        }
    }
}