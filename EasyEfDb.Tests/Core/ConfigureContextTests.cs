using EasyEfDb.Core;
using Microsoft.EntityFrameworkCore;



using NUnit.Framework;
using System;

namespace EasyEfDb.Tests.Core;

[TestFixture]
public class ConfigureContextTests
{
    public ConfigureContextTests()
    {
        
    }
    
    // write setup here/
    [SetUp]
    public void Setup()
    {
    }

    
    
    [Test]
    public void GetContext_ReturnsDbContext()
    {
        // Arrange
        var configureContext = new ConfigureContext();

        // Act
        var context = configureContext.GetContext<MyDbContext>( DatabaseType.InMemory, "databaseName");

        // Assert
        Assert.IsInstanceOf<DbContext>(context);
        //Assert.Pass();
    }
    
    [Test]
    public void CanAddUserToDatabase()
    {
        // Arrange
        var configureContext = new ConfigureContext();
        var context = configureContext.GetContext<MyDbContext>( DatabaseType.InMemory, "databaseName");
        var user = new User { Name = "John Doe", Email = "john.doe@example.com" };

        // Act
        context.Users.Add(user);
        context.SaveChanges();

        // Assert
        var savedUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
        Assert.IsNotNull(savedUser);
        Assert.AreEqual(user.Name, savedUser.Name);
        Assert.AreEqual(user.Email, savedUser.Email);
    }

    [Test]
    public void CanAddOrderToDatabase()
    {
        // Arrange
        var configureContext = new ConfigureContext();
        var context = configureContext.GetContext<MyDbContext>( DatabaseType.InMemory, "databaseName");
        var user = new User { Name = "Jane Doe", Email = "jane.doe@example.com" };
        

        // Act
        context.Users.Add(user);
        context.SaveChanges();
        
        
        var order = new Order { UserId = user.Id, OrderDate = DateTime.UtcNow };
        context.Orders.Add(order);
        context.SaveChanges();

        // Assert
        var savedOrder = context.Orders.FirstOrDefault(o => o.Id == order.Id);
        Assert.IsNotNull(savedOrder);
        Assert.AreEqual(user.Id, savedOrder.UserId);
        Assert.AreEqual(order.OrderDate, savedOrder.OrderDate);
    }

    [Test]
    public void CanAddProductToDatabase()
    {
        // Arrange
        var configureContext = new ConfigureContext();
        var context = configureContext.GetContext<MyDbContext>( DatabaseType.InMemory, "databaseName");
        var product = new Product { Name = "Product A", Price = 9.99m };

        // Act
        context.Products.Add(product);
        context.SaveChanges();

        // Assert
        var savedProduct = context.Products.FirstOrDefault(p => p.Id == product.Id);
        Assert.IsNotNull(savedProduct);
        Assert.AreEqual(product.Name, savedProduct.Name);
        Assert.AreEqual(product.Price, savedProduct.Price);
    }
}

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
    
    public MyDbContext(DbContextOptions<DbContext> options): base(options) 
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<Product> Products { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

