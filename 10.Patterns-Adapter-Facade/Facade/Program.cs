using Facade.Interfaces;
using Facade.Services;

namespace Facade
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductCatalog productCatalog = new ProductCatalogService();
            PaymentSystem paymentSystem = new PaymentSystemService();
            InvoiceSystem invoiceSystem = new InvoiceSystemService();

            OrderFacade orderFacade = new OrderFacade(productCatalog, paymentSystem, invoiceSystem);

            orderFacade.PlaceOrder("123", 2, "customer@example.com");
        }
    }
}
