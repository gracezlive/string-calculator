
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
            foreach (string arg in args)
            {
                if (arg == "--denyNegative" || arg == "-dn")
                {
                    allowNegative = false;
                }
                else
                {
                    delimiters.Add(arg);
                }
            }

            App app = new App(Configuration.StringParser, Configuration.Calculator);
            app.Run(delimiters.ToArray(), allowNegative);
        }
    }
}
