using Microsoft.EntityFrameworkCore;
using SHAKA.RestApi.EF.Entities;
using SHAKA.RestApi.EF.Example.API.Models;
using SHAKA.RestApi.EF.Extensions;

namespace SHAKA.RestApi.EF.Example.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<MyUser> MyUsers { get; set; } = null!;
    public DbSet<IdempotentRequest> IdempotentRequests { get; set; } = null!;    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {    
        base.OnModelCreating(modelBuilder);

        // Configure the IdempotentRequest entity
        modelBuilder.ConfigureIdempotentRequest();
    }
}
