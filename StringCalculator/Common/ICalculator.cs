using System.Collections.Generic;

namespace StringCalculator.Common
{
    public interface ICalculator
    {
        ICalculationResult Calculate(List<int> numbers, OperatorTypes mathOperator);
    }
}
