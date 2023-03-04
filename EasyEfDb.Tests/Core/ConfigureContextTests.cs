using EasyEfDb.Core;
using Microsoft.EntityFrameworkCore;



using NUnit.Framework;
using System;
using EasyEfDb.Tests.Test_Data.Contexts.Domain;
using EasyEfDb.Tests.Test_Tools;
using EasyEfDb.Tests.Test_Tools.ConfigDbs;
using TestContext = EasyEfDb.Tests.Test_Data.Contexts.TestContext;

namespace EasyEfDb.Tests.Core;

[TestFixture]
public class ConfigureContextTests
{
    private ConfigTestModel _configTestModel;
    // write setup here/
    public ConfigureContextTests()
    {
        _configTestModel = new ConfigTestModel();
    }

    // write setup here and run only once
    [OneTimeSetUp]
    public void Setup()
    {
        ILoadConfiguration loadConfiguration = new LoadConfiguration();
        _configTestModel = loadConfiguration.TryLoadConfigurationModel(out string error) ?? throw new Exception(error);
        // print if it worked till now
        Console.WriteLine($"ConfigTestModel: {_configTestModel.DbTypeFromConfig} {_configTestModel.DbName}");
    }
    
    [Test]
    public void GetContext_ReturnsDbContext()
    {
        // Arrange
        var configureContext = new ConfigureContext();

        // Act
        var context = configureContext.GetContext<TestContext>( _configTestModel.DbTypeFromConfig, _configTestModel.DbConnectionString);

        // Assert
        Assert.IsInstanceOf<DbContext>(context);
        //Assert.Pass();
    }
    
    [Test]
    public void CanAddUserToDatabase()
    {
        // Arrange
        var configureContext = new ConfigureContext();
        var context = configureContext.GetContext<TestContext>( DatabaseType.InMemory, "databaseName");
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
        var context = configureContext.GetContext<TestContext>( DatabaseType.InMemory, "databaseName");
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
        var context = configureContext.GetContext<TestContext>( DatabaseType.InMemory, "databaseName");
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