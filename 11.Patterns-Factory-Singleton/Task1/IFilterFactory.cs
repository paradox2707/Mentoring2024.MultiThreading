namespace Task1
{
    public interface IFilterFactory
    {
        IFilter CreateFilter(Bank bank, Country country);
    }
}
