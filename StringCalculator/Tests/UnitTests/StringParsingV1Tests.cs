using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using StringCalculator.Parsers.StringParsing;

namespace UnitTests
{
    [TestClass]
    public class StringParsingV1Tests
    {
        ParserV1 _parser;
        public StringParsingV1Tests()
        {
            _parser = new ParserV1();
        }

        [TestMethod]
        public void StringParsing()
        {
            _parser.Reset();

            string text = "23,10938423\r";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                _parser.Read(c);
            }

            List<int> numbers = _parser.GetNumbers();
            Assert.AreEqual(23, numbers[0]);
            Assert.AreEqual(10938423, numbers[1]);
        }

        [TestMethod]
        public void MaxSupportCheck()
        {
            _parser.Reset();

            try
            {
                string text = "1,2,3\r";
                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    _parser.Read(c);
                }

                List<int> numbers = _parser.GetNumbers();

                Assert.Fail("Failed to check for maximum support.");
            }
            catch (Exception exception)
            {
                if (!(exception is FormatException) || exception.Message != ParserV1.MAX_NUMBER_OF_NUMBERS_ERROR_MESSAGE)
                {
                    Assert.Fail("Expecting max support exception, but received a different exception: " + exception.Message);
                }
            }
        }

        [TestMethod]
        public void EmptyStringTest()
        {
            _parser.Reset();

            string text = "\r";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                _parser.Read(c);
            }

            List<int> numbers = _parser.GetNumbers();
            Assert.AreEqual(0, numbers[0]);
        }

        [TestMethod]
        public void InvalidNumbers()
        {
            _parser.Reset();

            string text = "sdfkj,43234\r";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                _parser.Read(c);
            }

            List<int> numbers = _parser.GetNumbers();
            Assert.AreEqual(0, numbers[0]);
            Assert.AreEqual(43234, numbers[1]);
        }

        [TestMethod]
        public void MissingNumbers()
        {
            _parser.Reset();

            string text = ",43234\r";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                _parser.Read(c);
            }

            List<int> numbers = _parser.GetNumbers();
            Assert.AreEqual(0, numbers[0]);
            Assert.AreEqual(43234, numbers[1]);
        }

        [TestMethod]
        public void Int32UpperBoundaryCheck()
        {
            _parser.Reset();

            string text = int.MaxValue.ToString() + "," + ((long)int.MaxValue + 1).ToString() + "\r";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                _parser.Read(c);
            }

            List<int> numbers = _parser.GetNumbers();
            Assert.AreEqual(int.MaxValue, numbers[0]);
            Assert.AreEqual(0, numbers[1]);
        }

        [TestMethod]
        public void Int32LowerBoundaryCheck()
        {
            _parser.Reset();

            string text = int.MinValue.ToString() + "," + ((long)int.MinValue - 1).ToString() + "\r";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                _parser.Read(c);
            }

            List<int> numbers = _parser.GetNumbers();
            Assert.AreEqual(int.MinValue, numbers[0]);
            Assert.AreEqual(0, numbers[1]);
        }
    }
}
