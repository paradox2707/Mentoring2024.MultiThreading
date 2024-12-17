using Task2.Interfaces;
using Task2.Models;

namespace Task2.Validators;

public class EmFeedValidator : IFeedValidator<EmFeed>
{
    public ValidateResult Validate(EmFeed feed)
    {
        var result = new ValidateResult();

        if (feed.StagingId < 1 || feed.CounterpartyId < 1 || feed.PrincipalId < 1 || feed.SourceAccountId < 1)
        {
            result.Errors.Add(ErrorCode.InvalidIdentifier);
        }

        if (feed.CurrentPrice < 0 || decimal.Round(feed.CurrentPrice, 2) != feed.CurrentPrice)
        {
            result.Errors.Add(ErrorCode.InvalidPrice);
        }

        if (feed.Sedol <= 0 || feed.Sedol >= 100)
        {
            result.Errors.Add(ErrorCode.InvalidSedol);
        }

        if (feed.AssetValue <= 0 || feed.AssetValue >= feed.Sedol)
        {
            result.Errors.Add(ErrorCode.InvalidAssetValue);
        }

        result.IsValid = result.Errors.Count == 0;
        return result;
    }
}
