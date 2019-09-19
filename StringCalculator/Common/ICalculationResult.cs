namespace StringCalculator.Common
{
    public interface ICalculationResult
    {
        long Numeric { get; set; }
        string Text { get; set; }
    }
}
