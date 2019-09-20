using System;
using System.Collections.Generic;
using System.Text;

using StringCalculator.Common;

namespace StringCalculator.Parsers.StringParsing
{
    public class ParserV4 : IStringParser
    {
        public static string AT_LEAST_ONE_DELIMITER_ERROR_MESSAGE = "At least 1 delimiter is expected.";
        public static string NO_NEGATIVE_ERROR_MESSAGE = "Negative numbers are not allowed: ";

        private List<string> _delimiters = new List<string>();
        private List<int> _numbers = new List<int>();
        private List<string> _negatives = new List<string>();
        private StringBuilder _stringBuilder = new StringBuilder();

        public ParserV4()
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
                    if (!AllowNegative && number < 0) _negatives.Add(number.ToString());
                    _stringBuilder.Clear();
                }
            }
            else
            {
                _stringBuilder.Append(c);
            }

            if (c == '\r' && _negatives.Count > 0)
            {
                string negativeNumbers = string.Join(", ", _negatives);
                throw new FormatException(NO_NEGATIVE_ERROR_MESSAGE + negativeNumbers);
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
            _numbers.Clear();
            _negatives.Clear();
            _stringBuilder.Clear();
        }

        /// <summary>
        /// When false, parse will throw a FormatException with a list of negative numbers passed in. Default value is true.
        /// </summary>
        public bool AllowNegative { get; set; } = true;

        public int? UpperBound { get; set; } = null;
    }
}
