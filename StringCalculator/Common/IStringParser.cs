using System.Collections.Generic;

namespace StringCalculator.Common
{
    public interface IStringParser
    {
        void SetDelimiters(char[] delimiters);
        void Read(char c);
        List<int> GetNumbers();
        void Reset();
    }
}
