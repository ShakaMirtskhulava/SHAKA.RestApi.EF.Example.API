using Microsoft.EntityFrameworkCore;
using SHAKA.RestApi.EF.Example.API.Data;
using SHAKA.RestApi.EF.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApiIdempotencyWithEF<ApplicationDbContext>(options =>
{
    options.IdempotencyHeaderName = "X-Idempotency-Key";
    options.DefaultExpirationTime = TimeSpan.FromDays(1);
    options.AllPostsAreIdempotent = true;
    options.AllPutsAreIdempotent = true;
    options.CleanupInterval = TimeSpan.FromDays(1);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseIdempotency();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
