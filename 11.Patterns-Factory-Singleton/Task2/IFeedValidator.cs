namespace Task2;

public interface IFeedValidator<T> where T : TradeFeed
{
    ValidateResult Validate(T feed);
}