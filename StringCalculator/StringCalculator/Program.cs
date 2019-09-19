
namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration.RegisterComponents();

            App app = new App(Configuration.StringParser, Configuration.Calculator);
            app.Run();
        }
    }
}
