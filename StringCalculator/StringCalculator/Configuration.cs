using StringCalculator.Calculators.BasicOperations;
using StringCalculator.Common;
using StringCalculator.Dependencies;
using StringCalculator.Parsers.StringParsing;

namespace StringCalculator
{
    static class Configuration
    {
        private static Resolver _resolver = null;

        public static void RegisterComponents()
        {
            Container container = new Container();
            container.RegisterType<ICalculator, Calculator>();
            container.RegisterType<IStringParser, ParserV3>();

            _resolver = new Resolver(container);
        }

        public static ICalculator Calculator
        {
            get
            {
                return (Calculator)_resolver.Resolve<ICalculator>();
            }
        }

        public static IStringParser StringParser
        {
            get
            {
                return (ParserV3)_resolver.Resolve<IStringParser>();
            }
        }
    }
}
