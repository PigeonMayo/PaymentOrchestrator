using Microsoft.EntityFrameworkCore;
using Payflex_Submission.Models;

namespace Payflex_Submission.Data
{
    public class ApiContext:DbContext
    {

        public DbSet<Payment> Payments { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }
    }
}
