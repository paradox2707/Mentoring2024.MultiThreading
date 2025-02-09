namespace Task2.Models;

public class DeltaOneFeed : TradeFeed
{
    public string Isin { get; set; }
    public DateTime MaturityDate { get; set; }
}