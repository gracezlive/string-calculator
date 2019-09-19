using System.Collections.Generic;

using StringCalculator.Common;

namespace StringCalculator.Calculators.BasicOperations
{
    public class Calculator : ICalculator
    {
        /// <summary>
        /// Applies a specific math operator on a list of numbers.
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="mathOperator"></param>
        /// <returns></returns>
        public ICalculationResult Calculate(List<int> numbers, OperatorTypes mathOperator)
        {
            ICalculationResult result = new Result
            {
                Numeric = 0,
                Text = ""
            };
            foreach (int number in numbers)
            {
                Operate(result, mathOperator, number);
            }
            
            if (!string.IsNullOrEmpty(result.Text)) result.Text += " = " + result.Numeric.ToString();
            return result;
        }

        private class Result : ICalculationResult
        {
            public long Numeric { get; set; }
            public string Text { get; set; }
        }

        private void Operate(ICalculationResult result, OperatorTypes mathOperator, int nextNumber)
        {
            string text = nextNumber < 0 ? "(" + nextNumber.ToString() + ")" : nextNumber.ToString();

            if (result.Text == "")
            {
                result.Numeric = nextNumber;
                result.Text = text;
                return;
            }

            switch (mathOperator)
            {
                case OperatorTypes.Add:
                    result.Numeric += nextNumber;
                    if (!string.IsNullOrEmpty(result.Text)) result.Text += "+";
                    break;
                case OperatorTypes.Subtract:
                    result.Numeric -= nextNumber;
                    if (!string.IsNullOrEmpty(result.Text)) result.Text += "-";
                    break;
                case OperatorTypes.Multiply:
                    result.Numeric *= nextNumber;
                    if (!string.IsNullOrEmpty(result.Text)) result.Text += "*";
                    break;
                case OperatorTypes.Divide:
                    result.Numeric /= nextNumber;
                    if (!string.IsNullOrEmpty(result.Text)) result.Text += "/";
                    break;
            }

            result.Text += text;
        }
    }
}
