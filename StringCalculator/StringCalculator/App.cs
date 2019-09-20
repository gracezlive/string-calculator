using System;
using System.Collections.Generic;

using StringCalculator.Common;

namespace StringCalculator
{
    public class App
    {
        IStringParser _parser;
        ICalculator _calculator;

        public App(IStringParser parser, ICalculator calculator)
        {
            _parser = parser ?? throw new ArgumentNullException("parser");
            _calculator = calculator ?? throw new ArgumentNullException("calculator");
        }

        public void Run(string[] delimiters, bool allowNegative = true, int? upperBound = null)
        {
            try
            {
                if (delimiters?.Length > 0)
                {
                    _parser.SetDelimiters(delimiters);
                }
                _parser.AllowNegative = allowNegative;
                _parser.UpperBound = upperBound;

                do
                {
                    try
                    {
                        _parser.Reset();

                        Console.Write("Please enter numbers: ");

                        int input;
                        do
                        {
                            input = Console.Read();

                            char ch;
                            try
                            {
                                ch = Convert.ToChar(input);
                            }
                            catch (OverflowException)
                            {
                                ch = 'a'; // causes entry to be 0
                            }

                            _parser.Read(ch);
                        }
                        while (input != 13);

                        List<int> numbers = _parser.GetNumbers();
                        ICalculationResult result = _calculator.Calculate(numbers, OperatorTypes.Add);
                        PrintResult(result.Text);
                    }
                    catch (FormatException formatException)
                    {
                        PrintError("Failed to process input: " + formatException.Message);
                        Console.ReadLine();
                    }
                    catch (Exception exception)
                    {
                        PrintError("Failed to read input: " + exception.Message);
                        Console.ReadLine();
                    }
                }
                while (true);
            }
            catch (Exception exception)
            {
                PrintError("Failed to run calculator: " + exception.Message);
            }
        }

        public void PrintResult(string message)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = previousColor;
            Console.WriteLine();
        }

        public void PrintError(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = previousColor;
            Console.WriteLine();
        }
    }
}
