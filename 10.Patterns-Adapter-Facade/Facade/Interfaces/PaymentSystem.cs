using Facade.Models;

namespace Facade.Interfaces
{
    public interface PaymentSystem
    {
        bool MakePayment(Payment payment);
    }
}
