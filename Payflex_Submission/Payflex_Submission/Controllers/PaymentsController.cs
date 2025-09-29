using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payflex_Submission.Models;
using Payflex_Submission.Services;

namespace Payflex_Submission.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public JsonResult Create(Payment payment)
        {
            var result = _paymentService.CreatePayment(payment);
            return new JsonResult(Ok(result));
        }

        [HttpPost]
        public JsonResult Edit(Payment payment)
        {
            var result = _paymentService.EditPayment(payment);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _paymentService.GetAllPayments();
            return new JsonResult(Ok(result));
        }

        [HttpPost]
        public JsonResult SimulateConfirmation(Guid id)
        {
            var result = _paymentService.SimulateConfirmation(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(new { message = "Payment confirmed", payment = result }));
        }
    }
}
