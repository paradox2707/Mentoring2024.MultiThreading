using Facade.Interfaces;
using Facade.Models;

namespace Facade.Services
{
    public class ProductCatalogService : ProductCatalog
    {
        public Product GetProductDetails(string productId)
        {
            // Simulate retrieving product details
            return new Product { Id = productId, Name = "Sample Product", Price = 99.99m };
        }
    }
}
