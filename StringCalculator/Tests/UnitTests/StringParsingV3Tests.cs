using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using StringCalculator.Parsers.StringParsing;

namespace UnitTests
{
    [TestClass]
    public class StringParsingV3Tests
    {
        ParserV3 _parser;
        public StringParsingV3Tests()
        {
            _parser = new ParserV3();
            _parser.SetDelimiters(new string[1] { "\n" });
        }

        [TestMethod]
        public void StringParsing()
        {
            _parser.Reset();

            string text = "23\n10938423,287382\n309874283,12837\r";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                _parser.Read(c);
            }

            List<int> numbers = _parser.GetNumbers();
            Assert.AreEqual(23, numbers[0]);
            Assert.AreEqual(10938423, numbers[1]);
            Assert.AreEqual(287382, numbers[2]);
            Assert.AreEqual(309874283, numbers[3]);
            Assert.AreEqual(12837, numbers[4]);
        }

        [TestMethod]
        public void MaxSupportCheck()
        {
            _parser.Reset();

            try
            {
                string text = "1,2\n3\r";
                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    _parser.Read(c);
                }

                List<int> numbers = _parser.GetNumbers();
            }
            catch (FormatException exception)
            {
                if (exception.Message == ParserV1.MAX_NUMBER_OF_NUMBERS_ERROR_MESSAGE)
                {
                    Assert.Fail("Expected unlimited number of numbers, but validation fired: " + exception.Message);
                }
                else
                {
                    throw exception;
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

            string text = "sdfkj,43234\n398723\r";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                _parser.Read(c);
            }

            List<int> numbers = _parser.GetNumbers();
            Assert.AreEqual(0, numbers[0]);
            Assert.AreEqual(43234, numbers[1]);
            Assert.AreEqual(398723, numbers[2]);
        }

        [TestMethod]
        public void MissingNumbers()
        {
            _parser.Reset();

            string text = "\n43234,,\r";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                _parser.Read(c);
            }

            List<int> numbers = _parser.GetNumbers();
            Assert.AreEqual(0, numbers[0]);
            Assert.AreEqual(43234, numbers[1]);
            Assert.AreEqual(0, numbers[2]);
            Assert.AreEqual(0, numbers[3]);
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
