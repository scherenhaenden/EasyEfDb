namespace EasyEfDb.Tests.Test_Tools;

public class EnvironmentLoader : IEnvironmentLoader
{
    public string? GetEnvironmentVariable(string environmentKey)
    {
        return Environment.GetEnvironmentVariable(environmentKey);
    }
}