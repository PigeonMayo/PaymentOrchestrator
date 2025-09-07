using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payflex_Submission.Controllers;
using Payflex_Submission.Data;
using Payflex_Submission.Models;
using System;
using Xunit;

namespace Payflex_Submission.UnitTests.TestControllers
{
    public class TestPaymentController : IDisposable
    {
        private readonly ApiContext _context;
        private readonly PaymentsController _controller;

        public TestPaymentController()
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApiContext(options);
            _controller = new PaymentsController(_context);
        }

        [Fact]
        public void Create_ValidPayment_ReturnsOk()
        {
            // Arrange
            var payment = new Payment
            {
                CustomerId = "HANK",
                Amount = 20
            };

            // Act
            var result = _controller.Create(payment);

            // Assert
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void Get_ExistingPayment_ReturnsOk()
        {
            
            var payment = new Payment { CustomerId = "HANK", Amount = 50 };
            _context.Payments.Add(payment);
            _context.SaveChanges();

            
            var result = _controller.Get(payment.Id);

            
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void Get_NonExistentPayment_ReturnsNotFound()
        {
            
            var result = _controller.Get(Guid.NewGuid());

            
            var jsonResult = (JsonResult)result;
            Assert.IsType<NotFoundResult>(jsonResult.Value);
        }

        [Fact]
        public void GetAll_ReturnsJsonResult()
        {
            
            var result = _controller.GetAll();

            
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void Delete_ExistingPayment_ReturnsNoContent()
        {
            
            var payment = new Payment { CustomerId = "HANK", Amount = 50 };
            _context.Payments.Add(payment);
            _context.SaveChanges();

            
            var result = _controller.Delete(payment.Id);

            
            var jsonResult = (JsonResult)result;
            Assert.IsType<NoContentResult>(jsonResult.Value);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}