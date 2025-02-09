using Task1.Models;

namespace Task1.Interfaces;

public interface IFilter
{
    IEnumerable<Trade> Match(IEnumerable<Trade> trades);
}