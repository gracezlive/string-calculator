using System;
using System.Collections.Generic;
using System.Text;

using StringCalculator.Common;

namespace StringCalculator.Parsers.StringParsing
{
    public class ParserV3 : IStringParser
    {
        public static string AT_LEAST_ONE_DELIMITER_ERROR_MESSAGE = "At least 1 delimiter is expected.";

        protected List<string> _delimiters = new List<string>();
        protected List<int> _numbers = new List<int>();
        protected StringBuilder _stringBuilder;

        public ParserV3()
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
        /// Reads one character at a time into the parser.
        /// </summary>
        /// <param name="c"></param>
        public void Read(char c)
        {
            if (_delimiters.Count == 0) throw new ArgumentException(AT_LEAST_ONE_DELIMITER_ERROR_MESSAGE + " Please call SetDelimiters method first.");

            int len = 0;
            if (c != '\r') len = ContainsDelimiter(_stringBuilder.ToString() + c);

            if (c == '\r' || len > 0)
            {
                string s = _stringBuilder.ToString();
                if (len > 0)
                {
                    s = s.Substring(0, s.Length - len + 1);
                }

                int number = 0;
                try
                {
                    int.TryParse(s, out number);
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

        private int ContainsDelimiter(string substring)
        {
            foreach (string d in _delimiters)
            {
                if (substring.EndsWith(d)) return d.Length;
            }
            return 0;
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
    }
}
