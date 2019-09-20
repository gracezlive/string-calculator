
using System;
using System.Collections.Generic;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration.RegisterComponents();

            List<string> delimiters = new List<string>();
            bool allowNegative = true;
            int? upperBound = null;
            foreach (string arg in args)
            {
                if (arg == "--denyNegative" || arg == "-dn")
                {
                    allowNegative = false;
                }
                else if (arg.StartsWith("--upperBound") || arg.StartsWith("-ub"))
                {
                    string[] parts = arg.Split('=');
                    if (parts.Length >= 2)
                    {
                        int value;
                        if (int.TryParse(parts[1], out value))
                        {
                            upperBound = value;
                        }
                    }
                }
                else
                {
                    delimiters.Add(arg);
                }
            }

            App app = new App(Configuration.StringParser, Configuration.Calculator);
            app.Run(delimiters.ToArray(), allowNegative, upperBound);
        }
    }
}
