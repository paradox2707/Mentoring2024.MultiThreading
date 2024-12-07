using Facade.Models;

namespace Facade.Interfaces
{
    public interface ProductCatalog
    {
        Product GetProductDetails(string productId);
    }
}
