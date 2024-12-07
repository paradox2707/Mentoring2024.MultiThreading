using Facade.Interfaces;
using Facade.Models;

namespace Facade.Services
{
    public class PaymentSystemService : PaymentSystem
    {
        public bool MakePayment(Payment payment)
        {
            // Simulate payment processing
            Console.WriteLine($"Payment of {payment.Amount}$ for product {payment.ProductId} processed.");
            return true;
        }
    }
}
