using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using StringCalculator.Parsers.StringParsing;

namespace UnitTests
{
    [TestClass]
    public class StringParsingV6Tests
    {
        ParserV6 _parser;
        public StringParsingV6Tests()
        {
            _parser = new ParserV6();
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
        public void InlineDelimiterTest()
        {
            _parser.Reset();

            string text = "//;,458\n-29,71;\n-283,;8\r";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                _parser.Read(c);
            }

            List<int> numbers = _parser.GetNumbers();
            Assert.AreEqual(458, numbers[0]);
            Assert.AreEqual(-29, numbers[1]);
            Assert.AreEqual(71, numbers[2]);
            Assert.AreEqual(0, numbers[3]);
            Assert.AreEqual(-283, numbers[4]);
            Assert.AreEqual(0, numbers[5]);
            Assert.AreEqual(8, numbers[6]);
        }

        [TestMethod]
        public void InvalidInlineDelimiterTest()
        {
            _parser.Reset();

            try
            {
                string text = "//yy,23,43\r";
                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    _parser.Read(c);
                }

                Assert.Fail("Failed to detect invalid inline delimiter format.");
            }
            catch (NotSupportedException exception)
            {
                if (exception.Message != ParserV6.UNSUPPORTED_INLINE_DELIMITER_FORMAT_ERROR_MESSAGE)
                {
                    Assert.Fail("Expected invalid inline delimiter format detection, but received a different exception: " + exception.Message);
                }
            }
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

        [TestMethod]
        public void DenyNegativeNumbers()
        {
            _parser.Reset();

            _parser.AllowNegative = false;

            try
            {
                string text = "-32,98\n-3892,3728,-2832,\n123\r";
                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    _parser.Read(c);
                }

                Assert.Fail("Failed to enforce no negative numbers.");
            }
            catch(FormatException formatException)
            {
                if (!formatException.Message.StartsWith(ParserV4.NO_NEGATIVE_ERROR_MESSAGE))
                {
                    Assert.Fail("Expected enforcement of non-negative numbers, but received a different exception: " + formatException.Message);
                }
            }
            finally
            {
                _parser.AllowNegative = true;
            }
        }

        [TestMethod]
        public void UpperBoundCheck()
        {
            _parser.Reset();

            _parser.UpperBound = 50;

            try
            {
                string text = "23\n50,,28,51,11\n309874283,,-2838\r";
                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    _parser.Read(c);
                }

                List<int> numbers = _parser.GetNumbers();
                Assert.AreEqual(23, numbers[0]);
                Assert.AreEqual(50, numbers[1]);
                Assert.AreEqual(0, numbers[2]);
                Assert.AreEqual(28, numbers[3]);
                Assert.AreEqual(11, numbers[4]);
                Assert.AreEqual(0, numbers[5]);
            }
            finally
            {
                _parser.UpperBound = null;
            }
        }
    }
}
