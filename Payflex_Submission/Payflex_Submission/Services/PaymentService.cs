using Payflex_Submission.Data;
using Payflex_Submission.Models;

namespace Payflex_Submission.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApiContext _context;

        public PaymentService(ApiContext context)
        {
            _context = context;
        }

        public Payment CreatePayment(Payment payment)
        {
            // Business logic: Ensure status is Pending on creation
            payment.Status = "Pending";

            _context.Payments.Add(payment);
            _context.SaveChanges();

            return payment;
        }

        public Payment EditPayment(Payment payment)
        {
            var paymentInDb = _context.Payments.Find(payment.Id);

            if (paymentInDb == null)
                return null;

            paymentInDb.CustomerId = payment.CustomerId;
            paymentInDb.Amount = payment.Amount;
            paymentInDb.Status = payment.Status;

            _context.SaveChanges();

            return paymentInDb;
        }

        public Payment GetPayment(Guid id)
        {
            return _context.Payments.Find(id);
        }

        public List<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public bool DeletePayment(Guid id)
        {
            var payment = _context.Payments.Find(id);

            if (payment == null)
                return false;

            _context.Payments.Remove(payment);
            _context.SaveChanges();

            return true;
        }

        public Payment SimulateConfirmation(Guid id)
        {
            var payment = _context.Payments.Find(id);

            if (payment == null)
                return null;

            // Business logic: Update status to Confirmed
            payment.Status = "Confirmed";
            _context.SaveChanges();

            return payment;
        }
    }
}

