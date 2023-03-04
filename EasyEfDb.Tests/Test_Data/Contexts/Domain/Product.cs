namespace EasyEfDb.Tests.Test_Data.Contexts.Domain;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}