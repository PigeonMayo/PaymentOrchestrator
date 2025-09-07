using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payflex_Submission.Models;
using Payflex_Submission.Data;

namespace Payflex_Submission.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        public readonly ApiContext _context;

        public PaymentsController(ApiContext context) { 
            _context = context;
        }

        //Create
        [HttpPost]
        public JsonResult Create(Payment payment)
        {
        
            _context.Payments.Add(payment);
            _context.SaveChanges();

            return new JsonResult(Ok(payment));
            
        }
        //Edit
        [HttpPost]
        public JsonResult Edit(Payment payment)
        {

            var paymentInDb = _context.Payments.Find(payment.Id);

            //if doesnt exist throw error
            if (paymentInDb == null)
                return new JsonResult(ValidationProblem());

            paymentInDb.CustomerId = payment.CustomerId;
            paymentInDb.Amount = payment.Amount;
            paymentInDb.Status = payment.Status;
            _context.SaveChanges();
            return new JsonResult(Ok(paymentInDb));


        }

        //Get single
        [HttpGet]
        public JsonResult Get(Guid id)
        {
            var result = _context.Payments.Find(id);
            if (result == null) 
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }


        //Get all
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Payments.ToList();

            return new JsonResult(Ok(result));
        }

        //Delete
        [HttpDelete]
        public JsonResult Delete(Guid id) 
        {

            var result = _context.Payments.Find(id);
            if (result == null)
                return new JsonResult(NotFound());

            _context.Payments.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }


    }
}
