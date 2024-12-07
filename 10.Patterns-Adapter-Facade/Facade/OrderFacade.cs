using Facade.Interfaces;
using Facade.Models;

namespace Facade
{
    public class OrderFacade
    {
        private readonly ProductCatalog _productCatalog;
        private readonly PaymentSystem _paymentSystem;
        private readonly InvoiceSystem _invoiceSystem;

        public OrderFacade(ProductCatalog productCatalog, PaymentSystem paymentSystem, InvoiceSystem invoiceSystem)
        {
            _productCatalog = productCatalog;
            _paymentSystem = paymentSystem;
            _invoiceSystem = invoiceSystem;
        }

        public void PlaceOrder(string productId, int quantity, string email)
        {
            // Load product details
            Product product = _productCatalog.GetProductDetails(productId);

            // Calculate total amount
            decimal totalAmount = product.Price * quantity;

            // Make payment
            Payment payment = new Payment { Amount = totalAmount, ProductId = productId };
            if (_paymentSystem.MakePayment(payment))
            {
                // Send invoice
                Invoice invoice = new Invoice { ProductId = productId, Email = email, Amount = totalAmount };
                _invoiceSystem.SendInvoice(invoice);
            }
        }
    }
}
