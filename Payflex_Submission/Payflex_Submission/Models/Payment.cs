using System.ComponentModel.DataAnnotations;
namespace Payflex_Submission.Models
{
    public class Payment
    {
        public Payment()
        {
            Id = Guid.NewGuid(); 
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        [Required]
        public string CustomerId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        [Required]
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }
    }
}
