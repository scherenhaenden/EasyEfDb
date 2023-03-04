using EasyEfDb.Tests.Test_Tools.ConfigDbs;

namespace EasyEfDb.Tests.Test_Tools;

public interface ILoadConfiguration
{
    public ConfigTestModel? TryLoadConfigurationModel(out string error);
}

public interface IEnvironmentLoader
{
    public string? GetEnvironmentVariable(string environmentKey);
}

public class EnvironmentLoader : IEnvironmentLoader
{
    public string? GetEnvironmentVariable(string environmentKey)
    {
        return Environment.GetEnvironmentVariable(environmentKey);
    }
}

