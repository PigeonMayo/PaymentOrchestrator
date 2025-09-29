using Microsoft.EntityFrameworkCore;
using Payflex_Submission.Data;
using Payflex_Submission.Models;
using Payflex_Submission.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApiContext>
    (opt => opt.UseInMemoryDatabase("Paymentsdb"));

builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();


// Use CORS
app.UseCors("AllowLocalhost3000");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApiContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    //Check for db
    context.Database.EnsureCreated();

    // is the database populated
    if (!context.Payments.Any())
    { 
        //add example payments
        var initialPayments = new List<Payment>
        {
            new Payment
            {
                CustomerId = "JOHN",
                Amount = 100,
                Status = "Confirmed"
            },
            new Payment
            {
                CustomerId = "JOHN",
                Amount = 50,
                Status = "Pending"
            },
            new Payment
            {
                CustomerId = "JOHN",
                Amount = 50,
                Status = "Pending"
            },
            new Payment
            {
                CustomerId = "MARY",
                Amount = 1000,
                Status = "Confirmed"
            },
            new Payment
            {
                CustomerId = "MARY",
                Amount = 10,
                Status = "Pending"
            }

        };

        context.Payments.AddRange(initialPayments);
        context.SaveChanges();

        logger.LogInformation("Seeded {Count} payments", initialPayments.Count);
    }
    else
    {
        logger.LogInformation("Database already contains {Count} payments, skipping seed",
            context.Payments.Count());
    }
}

app.Run();
