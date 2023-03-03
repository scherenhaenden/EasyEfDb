using EasyEfDb.Tests.Core;
using EasyEfDb.Tests.Test_Data.Contexts.Domain;
using Microsoft.EntityFrameworkCore;

namespace EasyEfDb.Tests.Test_Data.Contexts;

public class TestContext : DbContext
{
    public TestContext(DbContextOptions<TestContext> options) : base(options)
    {
    }
    
    public TestContext(DbContextOptions<DbContext> options): base(options) 
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
}