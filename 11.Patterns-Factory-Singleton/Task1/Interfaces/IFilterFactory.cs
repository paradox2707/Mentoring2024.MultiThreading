using Task1.Enums;

namespace Task1.Interfaces;

public interface IFilterFactory
{
    IFilter CreateFilter(Bank bank, Country country);
}
