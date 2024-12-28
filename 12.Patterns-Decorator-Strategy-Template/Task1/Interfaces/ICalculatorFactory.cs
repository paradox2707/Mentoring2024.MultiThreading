
namespace Task1.Interfaces;

public interface ICalculatorFactory
{
    ICalculator CreateDynamicCalculator(params Func<ICalculator, ICalculator>[] decorators);
    ICalculator CreateCalculator();
    ICalculator CreateCachedCalculator();
    ICalculator CreateLoggingCalculator();
    ICalculator CreateRoundingCalculator();
}
