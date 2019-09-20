using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using StringCalculator.Common;

namespace StringCalculator.Parsers.StringParsing
{
    public class ParserV8 : IStringParser
    {
        public static string AT_LEAST_ONE_DELIMITER_ERROR_MESSAGE = "At least 1 delimiter is expected.";
        public static string NO_NEGATIVE_ERROR_MESSAGE = "Negative numbers are not allowed: ";
        public static string INVALID_DELIMITER_ERROR_MESSAGE = "Delimiter cannot be '//', as it defines an inline delimiter.";
        public static string UNSUPPORTED_INLINE_DELIMITER_FORMAT_ERROR_MESSAGE = "Inline delimiter format is not supported: ";

        private List<string> _delimiters = new List<string>();
        private List<string> _inlineDelimiters = new List<string>();
        private List<int> _numbers = new List<int>();
        private List<string> _negatives = new List<string>();
        private StringBuilder _stringBuilder = new StringBuilder();

        private bool _firstEntry = false;

        public ParserV8()
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

            if (_delimiters.Exists(s => s == "//"))
            {
                throw new ArgumentException(INVALID_DELIMITER_ERROR_MESSAGE);
            }
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

                if (_firstEntry && s.StartsWith("//"))
                {
                    if (s.Length == 2)
                    {
                        // -ignore
                        // inline delimiter is already supported and will be treated as an existing delimiter
                        // since user's intention is unknown
                    }
                    else if (s.Length == 3)
                    {
                        _inlineDelimiters.Add(s[2].ToString());
                    }
                    else
                    {
                        Regex p = new Regex(@"^//(\[([^\[\]]+)\])+$");
                        Match match = p.Match(s);
                        if (match.Success && match.Groups.Count == 3)
                        {
                            foreach (Capture capture in match.Groups[2].Captures)
                            {
                                _inlineDelimiters.Add(capture.Value);
                            }
                        }
                        else
                        {
                            throw new FormatException(string.Format(UNSUPPORTED_INLINE_DELIMITER_FORMAT_ERROR_MESSAGE + " {0}", s));
                        }
                    }
                    _stringBuilder.Clear();
                }
                else
                {
                    int number = 0;
                    try
                    {
                        int.TryParse(s, out number);
                    }
                    finally
                    {
                        if (!UpperBound.HasValue || number <= UpperBound.Value)
                        {
                            _numbers.Add(number);
                            if (!AllowNegative && number < 0) _negatives.Add(number.ToString());
                        }
                        _stringBuilder.Clear();
                    }
                }
                _firstEntry = false;
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
            foreach (string d in _inlineDelimiters)
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
            _inlineDelimiters.Clear();
            _numbers.Clear();
            _negatives.Clear();
            _stringBuilder.Clear();

            _firstEntry = true;
        }

        /// <summary>
        /// When false, parse will throw a FormatException with a list of negative numbers passed in. Default value is true.
        /// </summary>
        public bool AllowNegative { get; set; } = true;

        /// <summary>
        /// When set, the largest number cannot exceed upper bound.
        /// </summary>
        public int? UpperBound { get; set; } = null;
    }
}
