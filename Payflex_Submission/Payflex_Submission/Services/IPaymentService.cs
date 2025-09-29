using Payflex_Submission.Models;

namespace Payflex_Submission.Services
{
    public interface IPaymentService
    {
        Payment CreatePayment(Payment payment);
        Payment EditPayment(Payment payment);
        Payment GetPayment(Guid id);
        List<Payment> GetAllPayments();
        bool DeletePayment(Guid id);
        Payment SimulateConfirmation(Guid id);
    }
}
