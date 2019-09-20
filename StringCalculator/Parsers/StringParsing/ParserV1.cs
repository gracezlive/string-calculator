using System;
using System.Collections.Generic;
using System.Text;

using StringCalculator.Common;

namespace StringCalculator.Parsers.StringParsing
{
    public class ParserV1 : IStringParser
    {
        public static string AT_LEAST_ONE_DELIMITER_ERROR_MESSAGE = "At least 1 delimiter is expected.";
        public static string MAX_NUMBER_OF_NUMBERS_ERROR_MESSAGE = "A maximum of 2 numbers are supported.";

        private List<string> _delimiters = new List<string>();
        private List<int> _numbers = new List<int>();
        private StringBuilder _stringBuilder;

        public ParserV1()
        {
            Reset();

            _delimiters.Add(",");
        }

        /// <summary>
        /// Adds additional delimiters to the parser.
        /// </summary>
        /// <param name="delimiters"></param>
        public void SetDelimiters(string[] delimiters)
        {
            if (delimiters == null || delimiters.Length == 0) throw new ArgumentException(AT_LEAST_ONE_DELIMITER_ERROR_MESSAGE);

            _delimiters.AddRange(delimiters);
        }

        /// <summary>
        /// Reads one character at a time into the parser. There is a maximum limit of 2 numbers supported by the parser.
        /// </summary>
        /// <param name="c"></param>
        public void Read(char c)
        {
            if (_delimiters.Count == 0) throw new ArgumentException(AT_LEAST_ONE_DELIMITER_ERROR_MESSAGE + " Please call SetDelimiters method first.");
            if (_numbers.Count >= 2) throw new FormatException(MAX_NUMBER_OF_NUMBERS_ERROR_MESSAGE);

            if (c == '\r' || _delimiters.Contains(c.ToString()))
            {
                int number = 0;
                try
                {
                    int.TryParse(_stringBuilder.ToString(), out number);
                }
                finally
                {
                    _numbers.Add(number);
                    _stringBuilder.Clear();
                }
            }
            else
            {
                _stringBuilder.Append(c);
            }
        }

        /// <summary>
        /// Returns a list of numbers parsed from the characters.
        /// </summary>
        /// <returns></returns>
        public List<int> GetNumbers()
        {
            return _numbers;
        }

        /// <summary>
        /// Resets data cached by the parser.
        /// </summary>
        public void Reset()
        {
            _numbers = new List<int>();
            _stringBuilder = new StringBuilder();
        }

        public bool AllowNegative { get; set; } = true;
    }
}
