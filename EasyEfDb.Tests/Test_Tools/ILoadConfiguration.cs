using EasyEfDb.Tests.Test_Tools.ConfigDbs;

namespace EasyEfDb.Tests.Test_Tools;

public interface ILoadConfiguration
{
    public ConfigTestModel? TryLoadConfigurationModel(out string error);
}