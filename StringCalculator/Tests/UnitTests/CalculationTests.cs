using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using StringCalculator.Calculators.BasicOperations;
using StringCalculator.Common;

namespace UnitTests
{
    [TestClass]
    public class CalculationTests
    {
        Calculator _calculator;
        public CalculationTests()
        {
            _calculator = new Calculator();
        }

        [TestMethod]
        public void AdditionArithmeticTest()
        {
            List<int> numbers = new List<int>(new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            ICalculationResult result = _calculator.Calculate(numbers, OperatorTypes.Add);
            Assert.AreEqual(55, result.Numeric);
            Assert.AreEqual("1+2+3+4+5+6+7+8+9+10 = 55", result.Text);

            numbers = new List<int>(new int[10] { -4, 0, 2, -5, -2, 7, 100, -13, 99, -200 });
            result = _calculator.Calculate(numbers, OperatorTypes.Add);
            Assert.AreEqual(-16, result.Numeric);
            Assert.AreEqual("(-4)+0+2+(-5)+(-2)+7+100+(-13)+99+(-200) = -16", result.Text);
        }

        [TestMethod]
        public void AdditionBoundaryTest()
        {
            try
            {
                List<int> numbers = new List<int>(new int[2] { int.MaxValue, int.MaxValue });
                ICalculationResult result = _calculator.Calculate(numbers, OperatorTypes.Add);
                long sum = (long)int.MaxValue + (long)int.MaxValue;
                Assert.AreEqual(sum, result.Numeric);
                Assert.AreEqual(int.MaxValue.ToString() + "+" + int.MaxValue.ToString() + " = " + sum.ToString(), result.Text);
            }
            catch (Exception exception)
            {
                Assert.Fail("Large integer addition exception is unhandled. " + exception.Message);
            }
        }

        [TestMethod]
        public void SubtractionArithmeticTest()
        {
            List<int> numbers = new List<int>(new int[4] { 55,22,11,2 });
            ICalculationResult result = _calculator.Calculate(numbers, OperatorTypes.Subtract);
            Assert.AreEqual(20, result.Numeric);
            Assert.AreEqual("55-22-11-2 = 20", result.Text);

            numbers = new List<int>(new int[5] { 0, -10, 4, 99, -150 });
            result = _calculator.Calculate(numbers, OperatorTypes.Subtract);
            Assert.AreEqual(57, result.Numeric);
            Assert.AreEqual("0-(-10)-4-99-(-150) = 57", result.Text);
        }

        [TestMethod]
        public void SubtractionBoundaryTest()
        {
            try
            {
                List<int> numbers = new List<int>(new int[2] { int.MinValue, int.MaxValue });
                ICalculationResult result = _calculator.Calculate(numbers, OperatorTypes.Subtract);
                long difference = (long)int.MinValue - (long)int.MaxValue;
                Assert.AreEqual(difference, result.Numeric);
                Assert.AreEqual("(" + int.MinValue.ToString() + ")-" + int.MaxValue.ToString() + " = " + difference.ToString(), result.Text);
            }
            catch (Exception exception)
            {
                Assert.Fail("Small integer subtraction exception is unhandled. " + exception.Message);
            }
        }

        [TestMethod]
        public void MultiplicationArithmeticTest()
        {
            List<int> numbers = new List<int>(new int[5] { 1, 2, 3, 4, 5 });
            ICalculationResult result = _calculator.Calculate(numbers, OperatorTypes.Multiply);
            Assert.AreEqual(120, result.Numeric);
            Assert.AreEqual("1*2*3*4*5 = 120", result.Text);

            numbers = new List<int>(new int[5] { -3, 10, -28, -25, 4 });
            result = _calculator.Calculate(numbers, OperatorTypes.Multiply);
            Assert.AreEqual(-84000, result.Numeric);
            Assert.AreEqual("(-3)*10*(-28)*(-25)*4 = -84000", result.Text);
        }

        [TestMethod]
        public void MultiplicationBoundaryTest()
        {
            try
            {
                List<int> numbers = new List<int>(new int[2] { int.MaxValue, int.MaxValue });
                ICalculationResult result = _calculator.Calculate(numbers, OperatorTypes.Multiply);
                long product = (long)int.MaxValue * (long)int.MaxValue;
                Assert.AreEqual(product, result.Numeric);
                Assert.AreEqual(int.MaxValue.ToString() + "*" + int.MaxValue.ToString() + " = " + product.ToString(), result.Text);
            }
            catch (Exception exception)
            {
                Assert.Fail("Large integer multiplication exception is unhandled. " + exception.Message);
            }
        }

        [TestMethod]
        public void DivisionArithmeticTest()
        {
            List<int> numbers = new List<int>(new int[3] { 60, 5, 2 });
            ICalculationResult result = _calculator.Calculate(numbers, OperatorTypes.Divide);
            Assert.AreEqual(6, result.Numeric);
            Assert.AreEqual("60/5/2 = 6", result.Text);
        }
    }
}
