using Microsoft.EntityFrameworkCore;
using Payflex_Submission.Data;
using Payflex_Submission.Models;
using Payflex_Submission.Services;
using Xunit;

namespace Payflex_Submission.Tests
{
    public class PaymentServiceTests : IDisposable
    {
        private readonly ApiContext _context;
        private readonly PaymentService _service;

        public PaymentServiceTests()
        {
            // Create a fresh in-memory database for each test
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApiContext(options);
            _service = new PaymentService(_context);
        }

        [Fact]
        public void CreatePayment_ShouldSetStatusToPending()
        {
            // Arrange
            var payment = new Payment
            {
                CustomerId = "HANK",
                Amount = 50
            };

            // Act
            var result = _service.CreatePayment(payment);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pending", result.Status);
            Assert.Equal("HANK", result.CustomerId);
            Assert.Equal(50, result.Amount);
        }

        [Fact]
        public void CreatePayment_ShouldSaveToDatabase()
        {
            // Arrange
            var payment = new Payment
            {
                CustomerId = "HANK",
                Amount = 100
            };

            // Act
            var result = _service.CreatePayment(payment);

            // Assert
            var savedPayment = _context.Payments.Find(result.Id);
            Assert.NotNull(savedPayment);
            Assert.Equal("HANK", savedPayment.CustomerId);
        }

        [Fact]
        public void GetPayment_WhenPaymentExists_ShouldReturnPayment()
        {
            // Arrange
            var payment = new Payment { CustomerId = "HANK", Amount = 100 };
            _service.CreatePayment(payment);

            // Act
            var result = _service.GetPayment(payment.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(payment.Id, result.Id);
            Assert.Equal("HANK", result.CustomerId);
        }

        [Fact]
        public void GetPayment_WhenPaymentDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = _service.GetPayment(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAllPayments_ShouldReturnAllPayments()
        {
            // Arrange
            _service.CreatePayment(new Payment { CustomerId = "COOPER", Amount = 300 });
            _service.CreatePayment(new Payment { CustomerId = "CALEB", Amount = 250 });
            _service.CreatePayment(new Payment { CustomerId = "CLARA", Amount = 300 });

            // Act
            var result = _service.GetAllPayments();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetAllPayments_WhenNoPayments_ShouldReturnEmptyList()
        {
            // Act
            var result = _service.GetAllPayments();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void EditPayment_WhenPaymentExists_ShouldUpdatePayment()
        {
            // Arrange
            var payment = _service.CreatePayment(new Payment
            {
                CustomerId = "HANK",
                Amount = 100
            });

            // Act
            payment.CustomerId = "HANK";
            payment.Amount = 150;
            var result = _service.EditPayment(payment);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("HANK", result.CustomerId);
            Assert.Equal(150, result.Amount);
        }

        [Fact]
        public void EditPayment_WhenPaymentDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var nonExistentPayment = new Payment
            {
                Id = Guid.NewGuid(),
                CustomerId = "HANK",
                Amount = 100
            };

            // Act
            var result = _service.EditPayment(nonExistentPayment);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeletePayment_WhenPaymentExists_ShouldReturnTrueAndRemovePayment()
        {
            // Arrange
            var payment = _service.CreatePayment(new Payment
            {
                CustomerId = "HANK",
                Amount = 100
            });

            // Act
            var result = _service.DeletePayment(payment.Id);

            // Assert
            Assert.True(result);
            Assert.Null(_service.GetPayment(payment.Id));
        }

        [Fact]
        public void DeletePayment_WhenPaymentDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = _service.DeletePayment(nonExistentId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void SimulateConfirmation_WhenPaymentExists_ShouldUpdateStatusToConfirmed()
        {
            // Arrange
            var payment = _service.CreatePayment(new Payment
            {
                CustomerId = "HANK",
                Amount = 100
            });
            Assert.Equal("Pending", payment.Status);

            // Act
            var result = _service.SimulateConfirmation(payment.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Confirmed", result.Status);
            Assert.Equal(payment.Id, result.Id);
        }

        [Fact]
        public void SimulateConfirmation_WhenPaymentDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = _service.SimulateConfirmation(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void SimulateConfirmation_ShouldPersistStatusChange()
        {
            // Arrange
            var payment = _service.CreatePayment(new Payment
            {
                CustomerId = "HANK",
                Amount = 100
            });

            // Act
            _service.SimulateConfirmation(payment.Id);

            // Assert - Fetch again to verify it was saved
            var updatedPayment = _service.GetPayment(payment.Id);
            Assert.Equal("Confirmed", updatedPayment.Status);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

