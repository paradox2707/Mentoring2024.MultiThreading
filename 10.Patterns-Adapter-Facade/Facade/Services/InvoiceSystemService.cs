using Facade.Interfaces;
using Facade.Models;

namespace Facade.Services
{
    public class InvoiceSystemService : InvoiceSystem
    {
        public void SendInvoice(Invoice invoice)
        {
            // Simulate sending an invoice
            Console.WriteLine($"Invoice for {invoice.ProductId} sent to {invoice.Email}.");
        }
    }
}
