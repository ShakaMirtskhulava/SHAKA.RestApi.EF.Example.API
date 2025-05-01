using Microsoft.EntityFrameworkCore;
using SHAKA.RestApi.EF.Example.API.Data;
using SHAKA.RestApi.EF.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add idempotency services
builder.Services.AddApiIdempotencyWithEF<ApplicationDbContext>(options =>
{
    options.IdempotencyHeaderName = "X-Idempotency-Key";
    options.DefaultExpirationTime = TimeSpan.FromDays(1);
    options.AllPostsAreIdempotent = true;
    options.AllPutsAreIdempotent = true;
    options.CleanupInterval = TimeSpan.FromDays(1);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add the middleware
app.UseIdempotency();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
