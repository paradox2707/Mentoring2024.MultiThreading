namespace Task2;

public class DeltaOneFeedValidator : IFeedValidator<DeltaOneFeed>
{
    public ValidateResult Validate(DeltaOneFeed feed)
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

        if (!System.Text.RegularExpressions.Regex.IsMatch(feed.Isin, @"^[A-Z]{2}\d{10}$"))
        {
            result.Errors.Add(ErrorCode.InvalidIsin);
        }

        if (feed.MaturityDate <= feed.ValuationDate)
        {
            result.Errors.Add(ErrorCode.InvalidMaturityDate);
        }

        result.IsValid = result.Errors.Count == 0;
        return result;
    }
}
