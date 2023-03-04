namespace EasyEfDb.Tests.Test_Tools;

public interface IEnvironmentLoader
{
    public string? GetEnvironmentVariable(string environmentKey);
}