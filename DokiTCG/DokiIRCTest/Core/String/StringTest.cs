using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace DokiIRCTest.Core.String
{
    [TestClass]
    public class StringTest
    {
        private const char BOLD = '\x02';
        private const char ITALIC = '\x1F';
        private const char COLOUR = '\x03';
        private const char RESET = '\x0F';
        private const char REVERSE = '\x16';

        [TestMethod]
        public void StringRedTest()
        {
            string expected = COLOUR + "04Test" + COLOUR;
            string actual = "Test".Red();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StringBoldTest()
        {
            string expected = BOLD + "Test" + BOLD;
            string actual = "Test".Bold();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StringItalicTest()
        {
            string expected = ITALIC + "Test" + ITALIC;
            string actual = "Test".Italic();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StringReverseTest()
        {
            string expected = REVERSE + "Test" + REVERSE;
            string actual = "Test".Reverse();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StringBoldItalicTest()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}{1}{0}", ITALIC, BOLD, "Test");
            string expected = sb.ToString();
            string actual = "Test".Bold().Italic();
            Assert.AreEqual(expected, actual);
        }
    }
}