using System;
using System.Collections.Generic;
using System.Text;

using StringCalculator.Common;

namespace StringCalculator.Parsers.StringParsing
{
    public class ParserV2 : IStringParser
    {
        protected List<char> _delimiters = new List<char>();
        protected List<int> _numbers = new List<int>();
        protected StringBuilder _stringBuilder;

        public ParserV2()
        {
            Reset();

            _delimiters.Add(',');
        }

        /// <summary>
        /// Adds additional delimiters to the parser.
        /// </summary>
        /// <param name="delimiters"></param>
        public void SetDelimiters(char[] delimiters)
        {
            if (delimiters == null || delimiters.Length == 0) throw new ArgumentException("At least 1 delimiter is expected.");

            _delimiters.AddRange(delimiters);
        }

        /// <summary>
        /// Reads one character at a time into the parse.
        /// </summary>
        /// <param name="c"></param>
        public void Read(char c)
        {
            if (_delimiters.Count == 0) throw new ArgumentException("At least 1 delimiter is expected. Please call SetDelimiters method first.");

            if (c == '\r' || _delimiters.Contains(c))
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
    }
}
