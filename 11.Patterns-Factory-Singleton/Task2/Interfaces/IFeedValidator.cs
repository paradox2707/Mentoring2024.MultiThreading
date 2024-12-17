using Task2.Models;

namespace Task2.Interfaces;

public interface IFeedValidator<T> where T : TradeFeed
{
    ValidateResult Validate(T feed);
}