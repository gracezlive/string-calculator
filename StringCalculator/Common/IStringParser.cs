using System.Collections.Generic;

namespace StringCalculator.Common
{
    public interface IStringParser
    {
        void SetDelimiters(string[] delimiters);
        void Read(char c);
        List<int> GetNumbers();
        void Reset();
        bool AllowNegative { get; set; }
        int? UpperBound { get; set; }
    }
}
